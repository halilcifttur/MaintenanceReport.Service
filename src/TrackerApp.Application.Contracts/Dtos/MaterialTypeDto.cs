using System;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class MaterialTypeDto : EntityDto<Guid>
{
    public virtual string Name { get; set; }
}