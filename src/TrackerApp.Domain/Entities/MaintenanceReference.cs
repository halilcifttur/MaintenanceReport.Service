using System;
using Volo.Abp.Domain.Entities;

namespace TrackerApp.Entities;

public class MaintenanceReference : Entity<Guid>
{
    public virtual EnumMaintenanceType MaintenanceType { get; set; }
    public virtual Guid MaterialId { get; set; }
}