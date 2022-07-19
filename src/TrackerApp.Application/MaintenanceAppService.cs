using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TrackerApp.Dtos;
using TrackerApp.Entities;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;

namespace TrackerApp;

public class MaintenanceAppService :
    CrudAppService<Maintenance, MaintenanceDto, Guid, PagedMaintenanceDto, CreateMaintenanceDto,
        UpdateMaintenanceDto>, IMaintenanceAppService
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly IRepository<Maintenance, Guid> _maintenanceRepository;
    private readonly IRepository<MaintenanceEntity, Guid> _maintenanceEntityRepository;
    private readonly IRepository<MaintenanceReference, Guid> _maintenanceReferenceRepository;
    private readonly IRepository<MaintenanceReferenceEntity, Guid> _maintenanceReferenceEntityRepository;
    private readonly IRepository<Checkin, Guid> _checkinRepository;
    private readonly IRepository<Material, Guid> _materialRepository;
    private readonly IObjectMapper _objectMapper;

    public MaintenanceAppService(IGuidGenerator guidGenerator,
        IRepository<Maintenance, Guid> maintenanceRepository,
        IRepository<MaintenanceEntity, Guid> maintenanceEntityRepository,
        IRepository<MaintenanceReference, Guid> maintenanceReferenceRepository,
        IRepository<MaintenanceReferenceEntity, Guid> maintenanceReferenceEntityRepository,
        IRepository<Checkin, Guid> checkinRepository,
        IRepository<Material, Guid> materialRepository,
        IObjectMapper objectMapper) : base(maintenanceRepository)
    {
        _guidGenerator = guidGenerator;
        _maintenanceRepository = maintenanceRepository;
        _maintenanceEntityRepository = maintenanceEntityRepository;
        _maintenanceReferenceRepository = maintenanceReferenceRepository;
        _maintenanceReferenceEntityRepository = maintenanceReferenceEntityRepository;
        _checkinRepository = checkinRepository;
        _materialRepository = materialRepository;
        _objectMapper = objectMapper;
    }

    public override async Task<MaintenanceDto> CreateAsync(CreateMaintenanceDto input)
    {
        if (!input.MaterialId.HasValue)
        {
            var materialRef = await _materialRepository.FirstOrDefaultAsync(p => p.Name == input.MaterialName);
            if (materialRef == null)
                throw new UserFriendlyException("Material not found");

            input.MaterialId = materialRef.Id;
        }

        var maintenance = Maintenance.Create(_guidGenerator.Create(), input.MaterialId.Value, input.MaintenanceType);

        maintenance = await _maintenanceRepository.InsertAsync(maintenance);

        var reference = await _maintenanceReferenceRepository.FindAsync(p =>
            p.MaterialId == maintenance.MaterialId && p.MaintenanceType == maintenance.MaintenanceType);

        if (reference != null)
        {
            var entities =
                await _maintenanceReferenceEntityRepository.GetListAsync(p => p.MaintenanceReferenceId == reference.Id);


            var list = entities.Select(p =>
            {
                var entity = MaintenanceEntity.Create(_guidGenerator.Create(), p.CheckinId, maintenance.Id);
                return entity;
            }).ToList();

            await _maintenanceEntityRepository.InsertManyAsync(list);
        }

        return _objectMapper.Map<Maintenance, MaintenanceDto>(maintenance);
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"material.{nameof(Material.Name)}";
        }

        if (sorting.Contains("materialName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "materialName",
                "material.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"material.{sorting}";
    }
    
    public override async Task<PagedResultDto<MaintenanceDto>> GetListAsync(PagedMaintenanceDto input)
    { 
        //Get the IQueryable<Book> from the repository
        var queryable = await Repository.GetQueryableAsync();

        var query = from maintenance in queryable
            join material in await _materialRepository.GetQueryableAsync() on maintenance.MaterialId equals material.Id
            select new {maintenance, material};

        //Paging
        query = query
            .Where(p=>p.maintenance.MaintenanceType == input.MaintenanceType)
            .OrderByDescending(p=>p.maintenance.CreationTime)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        var maintenanceDtos = queryResult.Select(x =>
        {
            var maintenanceDto = ObjectMapper.Map<Maintenance, MaintenanceDto>(x.maintenance);
            maintenanceDto.MaterialName = x.material.Name;
            return maintenanceDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<MaintenanceDto>(
            totalCount,
            maintenanceDtos
        );
        
    }

    public override async Task<MaintenanceDto> GetAsync(Guid id)
    {
        var result = await base.GetAsync(id);

        var entities = await _maintenanceEntityRepository.GetListAsync(p => p.MaintenanceId == id);

        result.Entities = _objectMapper.Map<List<MaintenanceEntity>, List<MaintenanceEntityDto>>(entities);

        foreach (var entity in result.Entities)
        {
            var checkin = await _checkinRepository.FindAsync(p => p.Id == entity.CheckinId);
            entity.CheckinName = checkin.Name;
        }

        var material = await _materialRepository.FirstOrDefaultAsync(p => p.Id == result.MaterialId);

        if (material != null)
            result.MaterialName = material.Name;

        return result;
    }

    public override async Task<MaintenanceDto> UpdateAsync(Guid id, UpdateMaintenanceDto input)
    {
        input.IsChecked = input.IsChecked;
        input.Status = EnumStatus.Done;
        input.CheckedTime = Clock.Now;

        var result = await base.UpdateAsync(id, input);

        var updatedEntities = new List<MaintenanceEntity>();
        foreach (var entityDto in input.Entities)
        {
            var entity = await _maintenanceEntityRepository.FindAsync(p => p.Id == entityDto.Id);
            if (entity != null)
            {
                entity.IsChecked = entityDto.IsChecked;

                updatedEntities.Add(entity);
            }
        }

        if (updatedEntities.Count > 0)
            await _maintenanceEntityRepository.UpdateManyAsync(updatedEntities, true);

        result = await GetAsync(id);
        return result;
    }
}