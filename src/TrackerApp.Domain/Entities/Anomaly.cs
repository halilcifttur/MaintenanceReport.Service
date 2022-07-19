using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace TrackerApp.Entities;

public class Anomaly : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid MaterialId { get; set; }

    public virtual EnumStatus Status { get; set; }

    public virtual string Cause { get; set; }
    
    public virtual string Action { get; set; }

    public virtual DateTime? AnomalyStartDate { get; set; }
    public virtual DateTime? AnomalyEndDate { get; set; }
    
    public virtual DateTime? MaintenanceStartDate { get; set; }
    public virtual DateTime? MaintenanceEndDate { get; set; }
}