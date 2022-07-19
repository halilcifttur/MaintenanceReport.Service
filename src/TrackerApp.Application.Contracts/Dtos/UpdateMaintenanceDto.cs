using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class UpdateMaintenanceDto : EntityDto<Guid>
{
    public virtual Guid MaterialId { get; set; } 
    
    public virtual EnumMaintenanceType MaintenanceType { get; set; }

    public virtual EnumStatus Status { get; set; }
    
    public virtual int? KmHour { get; set; }
    
    public virtual bool? IsChecked { get; set; }
    
    public virtual DateTime? CheckedTime { get; set; } 
    
    public virtual IList<MaintenanceEntityDto> Entities { get; set; }
    
}