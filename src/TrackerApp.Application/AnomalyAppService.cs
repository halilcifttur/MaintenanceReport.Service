using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TrackerApp.Dtos;
using TrackerApp.Entities;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TrackerApp;

public class AnomalyAppService :
    CrudAppService<Anomaly, AnomalyDto, Guid, PagedAnomalyDto, CreateAnomalyDto, UpdateAnomalyDto>,
    IAnomalyAppService
{
    private readonly IRepository<Material, Guid> _materialRepository;

    public AnomalyAppService(IRepository<Anomaly, Guid> repository, IRepository<Material, Guid> materialRepository) :
        base(repository)
    {
        _materialRepository = materialRepository;
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

    public override async Task<PagedResultDto<AnomalyDto>> GetListAsync(PagedAnomalyDto input)
    { 
        //Get the IQueryable<Book> from the repository
        var queryable = await Repository.GetQueryableAsync();

        var query = from anomaly in queryable
            join material in await _materialRepository.GetQueryableAsync() on anomaly.MaterialId equals material.Id
            select new {anomaly, material};

        //Paging
        query = query
            .Where(p=>p.anomaly.Status == input.AnomalyStatus)
            .OrderByDescending(p=>p.anomaly.CreationTime)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        var maintenanceDtos = queryResult.Select(x =>
        {
            var maintenanceDto = ObjectMapper.Map<Anomaly, AnomalyDto>(x.anomaly);
            maintenanceDto.MaterialName = x.material.Name;
            return maintenanceDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<AnomalyDto>(
            totalCount,
            maintenanceDtos
        );
        
    }
    public override async Task<AnomalyDto> CreateAsync(CreateAnomalyDto input)
    {
        if (!input.MaterialId.HasValue)
        {
            var materialRef = await _materialRepository.FirstOrDefaultAsync(p => p.Name == input.MaterialName);
            if(materialRef == null)
                throw new UserFriendlyException("Material not found");
            
            input.MaterialId = materialRef.Id;
        }
        
        if (input.Cause.Length == 0)
        {
            throw new UserFriendlyException("Cause cannot be empty");
        }

        if (input.AnomalyStartDate == null)
        {
            input.AnomalyStartDate = Clock.Now;
        }

        var result = await base.CreateAsync(input);

        return result;
    }

    public override async Task<AnomalyDto> UpdateAsync(Guid id, UpdateAnomalyDto input)
    {
        if (input.Action.Length == 0)
        {
            throw new UserFriendlyException("Action cannot be empty");
        }

        var now = Clock.Now;

        if (input.MaintenanceStartDate == null)
        {
            input.MaintenanceStartDate = now;
        }

        if (input.Status == EnumStatus.Done)
        {
            if (input.MaintenanceEndDate == null)
                input.MaintenanceEndDate = now;


            input.AnomalyEndDate = input.MaintenanceEndDate;
        }

        if (input.MaintenanceStartDate == input.MaintenanceEndDate)
        {
            throw new UserFriendlyException("Maintenance start date cannot be equal to maintenance end date");
        }

        var result = await base.UpdateAsync(id, input);

        return result;
    }

    public override async Task<AnomalyDto> GetAsync(Guid id)
    {
        var result = await base.GetAsync(id);

        var material = await _materialRepository.FirstOrDefaultAsync(p => p.Id == result.MaterialId);

        if (material != null)
            result.MaterialName = material.Name;

        return result;
    }
}