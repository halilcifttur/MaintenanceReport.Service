using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace TrackerApp.Entities;

public class Material : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid MaterialType { get; set; }

    public virtual string Name { get; set; }
    public virtual string SerialNumber { get; set; }
    
    public Guid? TenantId { get; set; }
    
    public static Material Create(Guid id, Guid materialType, string name, string serialNumber)
    {
        return new Material
        {
            Id = id,
            MaterialType = materialType,
            Name = name,
            SerialNumber = serialNumber
        };
    }
}