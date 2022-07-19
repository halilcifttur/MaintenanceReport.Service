using System;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class UpdateAnomalyDto : EntityDto<Guid>
{
    public virtual EnumStatus Status { get; set; }
    
    public virtual string Action { get; set; }

    public virtual DateTime? AnomalyEndDate { get; set; }
    public virtual DateTime? MaintenanceStartDate { get; set; }
    public virtual DateTime? MaintenanceEndDate { get; set; }
}