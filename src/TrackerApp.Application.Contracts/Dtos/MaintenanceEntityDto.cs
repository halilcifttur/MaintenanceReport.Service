using System;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class MaintenanceEntityDto : EntityDto<Guid?>
{
    public virtual Guid CheckinId { get; set; }
    public virtual string CheckinName { get; set; }
    
    public virtual bool? IsChecked { get; set; }
}