using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TrackerApp.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace TrackerApp.Blazor.Pages;

public partial class Anomaly
{
    private List<MaterialDto> _materials = new List<MaterialDto>();

    [Inject] protected IMaterialAppService MaterialAppService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetMaterialsAsync();
    }

    private async Task GetMaterialsAsync()
    {
        var list = await MaterialAppService.GetListAsync(new PagedAndSortedResultRequestDto() {MaxResultCount = 999});

        _materials = list.Items.ToList();
    }
}