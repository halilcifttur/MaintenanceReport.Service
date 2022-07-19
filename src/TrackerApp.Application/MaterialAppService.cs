using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TrackerApp.Dtos;
using TrackerApp.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.TenantManagement;

namespace TrackerApp;
 
[Authorize]
public class MaterialAppService : CrudAppService<Material, MaterialDto, Guid, PagedAndSortedResultRequestDto, CreateOrUpdateMaterialDto>, IMaterialAppService
{
    private readonly IRepository<Material, Guid> _repository;
    private readonly IRepository<MaterialType, Guid> _materialTypeRepository;
    private readonly IRepository<Tenant, Guid> _tenantRepository;

    public MaterialAppService(IRepository<Material, Guid> repository, IRepository<MaterialType,Guid> _materialTypeRepository, IRepository<Tenant, Guid> _tenantRepository) : base(repository)
    {
        _repository = repository;
        this._materialTypeRepository = _materialTypeRepository;
        this._tenantRepository = _tenantRepository;
    }

    public override async Task<MaterialDto> CreateAsync(CreateOrUpdateMaterialDto input)
    {
        await CheckCreatePolicyAsync();

        var entity = await MapToEntityAsync(input);

        TryToSetTenantId(entity);

        if (input.TenantId.HasValue)
        {
            entity.TenantId = input.TenantId;
        }

        await Repository.InsertAsync(entity, autoSave: true);

        return await MapToGetOutputDtoAsync(entity);
    }

    public override async Task<MaterialDto> GetAsync(Guid id)
    {
        var queryable = await Repository.GetQueryableAsync();

        var query = from material in queryable
            join materialType in await _materialTypeRepository.GetQueryableAsync() on material.MaterialType equals materialType.Id
            where material.Id == id
            select new { material, materialType };

        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Material), id);
        }

        var materialDto = ObjectMapper.Map<Material, MaterialDto>(queryResult.material);
        materialDto.MaterialTypeName = queryResult.materialType.Name;

        if (materialDto.TenantId.HasValue)
        {
            var tenant = await _tenantRepository.FirstOrDefaultAsync(p => p.Id == materialDto.TenantId.Value);

            if (tenant != null)
                materialDto.TenantName = tenant.Name;
        }
        
        return materialDto;
    }
} 