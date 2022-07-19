using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TrackerApp.Dtos;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Blazor.Pages;

public partial class MaintenanceReference
{
    private List<MaterialDto> _materials = new List<MaterialDto>();
    private List<CheckinDto> _checkins = new List<CheckinDto>();

    [Inject] protected IMaterialAppService MaterialAppService { get; set; }
    [Inject] protected ICheckinAppService CheckinAppService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetMaterialsAsync();
        await GetCheckinsAsync();
    }

    private async Task GetMaterialsAsync()
    {
        var list = await MaterialAppService.GetListAsync(new PagedAndSortedResultRequestDto() {MaxResultCount = 999});

        _materials = list.Items.ToList();
    }

    private async Task GetCheckinsAsync()
    {
        var list = await CheckinAppService.GetListAsync(new PagedAndSortedResultRequestDto() {MaxResultCount = 999});

        _checkins = list.Items.ToList();
    }

    private bool IsCheckinAdded(CheckinDto checkin)
    {
        EditingEntity.Entities ??= new List<Guid>();
        
        return EditingEntity.Entities.Any(p => p == checkin.Id);
    }

    private void CheckinChanged(bool isAdded, CheckinDto checkin)
    {
        EditingEntity.Entities ??= new List<Guid>();
        
        if (isAdded) EditingEntity.Entities.Add(checkin.Id);
        else EditingEntity.Entities.Remove(checkin.Id);
    }

    private string GetShownName(CheckinDto checkin)
    {
        return checkin.Name;
    }
}