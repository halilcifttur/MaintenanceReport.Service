using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Bootstrap5;
using Microsoft.AspNetCore.Components;
using TrackerApp.Dtos;
using Volo.Abp.Application.Dtos;

namespace TrackerApp.Blazor.Pages;

public partial class Maintenance
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

    private bool IsChecked(MaintenanceEntityDto checkin)
    {
        EditingEntity.Entities ??= new List<MaintenanceEntityDto>();
        
        var entity = EditingEntity.Entities.FirstOrDefault(p => p.CheckinId == checkin.CheckinId);
        
        return entity?.IsChecked ?? false;
    }

    private void CheckinChanged(bool? isChecked, MaintenanceEntityDto checkin)
    {
        EditingEntity.Entities ??= new List<MaintenanceEntityDto>();

        var entity = EditingEntity.Entities.FirstOrDefault(p => p.CheckinId == checkin.CheckinId);

        if (entity == null)
        {
            EditingEntity.Entities.Add(new MaintenanceEntityDto
            {
                CheckinId = checkin.Id.Value,
                IsChecked = isChecked,
            });
        }
        else
        {
            entity.IsChecked = isChecked;
        }
    }

    private string GetShownName(Guid checkinId)
    {
        return _checkins.FirstOrDefault(p=>p.Id == checkinId)!.Name;
    }
}