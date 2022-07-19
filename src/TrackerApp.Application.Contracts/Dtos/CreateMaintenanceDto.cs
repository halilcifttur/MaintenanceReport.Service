using System;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class CreateMaintenanceDto
{
    public virtual Guid? MaterialId { get; set; } 
    public virtual string MaterialName { get; set; } 
    
    public virtual EnumMaintenanceType MaintenanceType { get; set; }
}