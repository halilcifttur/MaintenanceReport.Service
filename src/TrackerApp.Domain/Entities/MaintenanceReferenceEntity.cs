using System;
using Volo.Abp.Domain.Entities;

namespace TrackerApp.Entities;

public class MaintenanceReferenceEntity : Entity<Guid>
{
    public virtual Guid MaintenanceReferenceId { get; set; }
    public virtual Guid CheckinId { get; set; }
    
    public static MaintenanceReferenceEntity Create(Guid id, Guid maintenanceReferenceId, Guid checkinId)
    {
        var maintenanceReference = new MaintenanceReferenceEntity
        {
            MaintenanceReferenceId = maintenanceReferenceId,
            CheckinId = checkinId,
            Id = id
        };

        return maintenanceReference;
    }
}