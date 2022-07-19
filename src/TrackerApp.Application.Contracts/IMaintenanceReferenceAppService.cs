using System;
using TrackerApp.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TrackerApp;

public interface IMaintenanceReferenceAppService : ICrudAppService<MaintenanceReferenceDto,Guid,PagedAndSortedResultRequestDto,CreateOrUpdateMaintenanceReferenceDto>
{
    
}