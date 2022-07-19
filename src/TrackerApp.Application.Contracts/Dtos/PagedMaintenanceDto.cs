using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class PagedMaintenanceDto : PagedAndSortedResultRequestDto
{
    public virtual EnumMaintenanceType MaintenanceType { get; set; }
}