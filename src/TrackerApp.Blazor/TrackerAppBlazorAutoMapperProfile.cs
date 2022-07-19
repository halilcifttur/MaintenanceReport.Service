using AutoMapper;
using TrackerApp.Dtos;

namespace TrackerApp.Blazor;

public class TrackerAppBlazorAutoMapperProfile : Profile
{
    public TrackerAppBlazorAutoMapperProfile()
    {
        CreateMap<MaterialDto, CreateOrUpdateMaterialDto>();
        CreateMap<MaterialTypeDto, CreateOrUpdateMaterialTypeDto>();
        CreateMap<CheckinDto, CreateOrUpdateCheckinDto>();
        CreateMap<MaintenanceReferenceDto, CreateOrUpdateMaintenanceReferenceDto>();
        
        CreateMap<AnomalyDto, UpdateAnomalyDto>();
        
        CreateMap<MaintenanceDto, CreateMaintenanceDto>();
        CreateMap<MaintenanceDto, UpdateMaintenanceDto>();
    }
}
