using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TrackerApp.Dtos;
using TrackerApp.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace TrackerApp; 

[Authorize]
public class MaterialTypeAppService : CrudAppService<MaterialType, MaterialTypeDto, Guid, MaterialTypeListDto, CreateOrUpdateMaterialTypeDto>, IMaterialTypeAppService
{
    public MaterialTypeAppService(IRepository<MaterialType, Guid> repository) : base(repository)
    { 
    }
}