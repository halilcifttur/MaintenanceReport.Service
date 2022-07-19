using System;
using Volo.Abp.Domain.Entities;

namespace TrackerApp.Entities;

public class MaterialType : Entity<Guid>
{
    public virtual string Name { get; set; }
    
    public static MaterialType Create(Guid guid, string name)
    {
        return new MaterialType
        {
            Id = guid,
            Name = name
        };
    }
}