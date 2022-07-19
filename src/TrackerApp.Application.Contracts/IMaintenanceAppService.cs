using System;
using TrackerApp.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TrackerApp;

public interface IMaintenanceAppService : ICrudAppService<MaintenanceDto,Guid,PagedMaintenanceDto,CreateMaintenanceDto,UpdateMaintenanceDto>
{
    
}