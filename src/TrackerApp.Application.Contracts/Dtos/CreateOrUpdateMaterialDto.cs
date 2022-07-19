using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace TrackerApp.Dtos;

public class CreateOrUpdateMaterialDto : EntityDto<Guid?> ,IMultiTenant
{
    public virtual Guid MaterialType { get; set; }

    public virtual string Name { get; set; }
    public virtual string SerialNumber { get; set; }

    public virtual Guid? TenantId { get; set; }
} 