using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackerApp.Dtos;
using TrackerApp.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace TrackerApp;

public class MaintenanceReferenceAppService :
    CrudAppService<MaintenanceReference, MaintenanceReferenceDto, Guid, PagedAndSortedResultRequestDto,
        CreateOrUpdateMaintenanceReferenceDto>, IMaintenanceReferenceAppService
{
    private readonly IRepository<MaintenanceReferenceEntity, Guid> _maintenanceReferenceEntityRepository;
    private readonly IGuidGenerator _guidGenerator;

    public MaintenanceReferenceAppService(IRepository<MaintenanceReference, Guid> repository, 
        IRepository<MaintenanceReferenceEntity, Guid> maintenanceReferenceEntityRepository,
        IGuidGenerator guidGenerator) : base(repository)
    {
        _maintenanceReferenceEntityRepository = maintenanceReferenceEntityRepository;
        _guidGenerator = guidGenerator;
    }

    public override async Task<MaintenanceReferenceDto> GetAsync(Guid id)
    {
        var result =  await base.GetAsync(id);
        
        var list = await _maintenanceReferenceEntityRepository.GetListAsync(x => x.MaintenanceReferenceId == id);

        result.Entities = list.Select(p => p.CheckinId).ToList();

        return result;
    }

    public override async Task<MaintenanceReferenceDto> UpdateAsync(Guid id, CreateOrUpdateMaintenanceReferenceDto input)
    {
        var result =  await base.UpdateAsync(id, input);

        if (result != null)
        {
            await _maintenanceReferenceEntityRepository.DeleteAsync(p => p.MaintenanceReferenceId == id);
            
            var list = new List<MaintenanceReferenceEntity>();
            list.AddRange(input.Entities.Select(p =>
            {
                var newGuid = _guidGenerator.Create();
                return MaintenanceReferenceEntity.Create(newGuid, id, p);
            }));
            
            await _maintenanceReferenceEntityRepository.InsertManyAsync(list, true);
        }
 
        return result;
    }
}