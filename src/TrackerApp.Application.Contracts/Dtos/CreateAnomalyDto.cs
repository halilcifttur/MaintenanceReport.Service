using System;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class CreateAnomalyDto
{
    public virtual Guid? MaterialId { get; set; }
    public virtual string MaterialName { get; set; }

    public virtual string Cause { get; set; }

    public virtual DateTime? AnomalyStartDate { get; set; }
    
    public virtual Guid? TenantId { get; set; }
}