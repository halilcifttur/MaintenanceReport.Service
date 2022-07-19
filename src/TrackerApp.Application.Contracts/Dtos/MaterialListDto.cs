using System;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Dtos;

public class MaterialListDto : EntityDto<Guid>
{
    public virtual Guid MaterialType { get; set; }

    public virtual string Name { get; set; }
    public virtual string SerialNumber { get; set; }
}