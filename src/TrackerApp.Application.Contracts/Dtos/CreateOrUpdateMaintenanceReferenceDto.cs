using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class CreateOrUpdateMaintenanceReferenceDto : EntityDto<Guid?>
{
    public virtual EnumMaintenanceType MaintenanceType { get; set; }
    public virtual Guid MaterialId { get; set; }
    
    public virtual IList<Guid> Entities { get; set; }
}