using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TrackerApp.Dtos;
using Volo.Abp.TenantManagement;

namespace TrackerApp.Blazor.Pages;

public partial class Material
{
    private List<MaterialTypeDto> _materialTypes = new List<MaterialTypeDto>();
    private List<TenantDto> _tenants = new List<TenantDto>();
    
    [Inject] protected ITenantAppService TenantAppService { get; set; }
    
    [Inject] protected IMaterialTypeAppService MaterialTypeAppService { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await GetMaterialTypesAsync();
        await GetTenantsAsync();
    }


    private async Task GetTenantsAsync()
    {
        var list = await TenantAppService.GetListAsync( new GetTenantsInput()
        {
            MaxResultCount = 1000
        });

        _tenants = list.Items.ToList();
    }
    private async Task GetMaterialTypesAsync()
    {
        var list = await MaterialTypeAppService.GetListAsync(new MaterialTypeListDto() { });

        _materialTypes = list.Items.ToList(); 
    }
}