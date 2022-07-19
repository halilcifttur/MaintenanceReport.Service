using System;
using TrackerApp.Dtos;
using Volo.Abp.Application.Services;

namespace TrackerApp;

public interface IMaterialTypeAppService : ICrudAppService<MaterialTypeDto, Guid, MaterialTypeListDto, CreateOrUpdateMaterialTypeDto>
{
    
}