using Volo.Abp.TenantManagement;

namespace TrackerApp.Dtos;

public class MyTenantUpdateDto : TenantUpdateDto
{
    public virtual string Country { get; set; }
    public virtual string City { get; set; }
}