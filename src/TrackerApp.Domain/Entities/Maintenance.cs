using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace TrackerApp.Entities;

public class Maintenance : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid MaterialId { get; set; }
    
    public virtual EnumMaintenanceType MaintenanceType { get; set; }

    public virtual EnumStatus Status { get; set; }
    
    public virtual int? KmHour { get; set; }
    
    public virtual bool? IsChecked { get; set; }
    
    public virtual DateTime? CheckedTime { get; set; } 

    public static Maintenance Create(Guid id, Guid materialId, EnumMaintenanceType maintenanceType)
    {
        var @maintenance = new Maintenance
        {
            Id = id,
            MaterialId = materialId,
            MaintenanceType = maintenanceType,
            Status = EnumStatus.New,
            IsChecked = null
        };

        return @maintenance;
    }
}