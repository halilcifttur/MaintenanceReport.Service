using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace TrackerApp.Entities;

public class Checkin : Entity<Guid>, ISoftDelete
{
    public virtual string Name { get; set; }
    
    public bool IsDeleted { get; set; }
}