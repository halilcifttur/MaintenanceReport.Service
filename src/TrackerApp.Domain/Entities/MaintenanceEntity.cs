using System;
using Volo.Abp.Domain.Entities;

namespace TrackerApp.Entities;

public class MaintenanceEntity : Entity<Guid>
{
    public virtual Guid MaintenanceId { get; set; }
    
    public virtual Guid CheckinId { get; set; }
    
    public virtual bool? IsChecked { get; set; }

    public static MaintenanceEntity Create(Guid id, Guid checkinId, Guid maintenanceId)
    {
        return new MaintenanceEntity()
        { 
            Id = id,
            CheckinId = checkinId,
            MaintenanceId = maintenanceId,
            IsChecked = false
        };
    }
}