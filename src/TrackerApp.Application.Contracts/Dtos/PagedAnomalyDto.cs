using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class PagedAnomalyDto : PagedAndSortedResultRequestDto
{
    public virtual EnumStatus AnomalyStatus { get; set; }
}