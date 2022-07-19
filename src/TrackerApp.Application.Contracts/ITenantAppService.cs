using System;
using System.Threading.Tasks;
using TrackerApp.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.TenantManagement;

namespace TrackerApp;
/*
public interface ITenantAppService : ICrudAppService<TenantDto, Guid, GetTenantsInput, MyTenantCreateDto, MyTenantUpdateDto>
{
    Task<string> GetDefaultConnectionStringAsync(Guid id);

    Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString);

    Task DeleteDefaultConnectionStringAsync(Guid id);
}*/