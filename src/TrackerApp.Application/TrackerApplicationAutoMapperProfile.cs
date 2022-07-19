using AutoMapper;
using TrackerApp.Dtos;
using TrackerApp.Entities;
using Volo.Abp.AutoMapper;
using Volo.Abp.TenantManagement;

namespace TrackerApp;

public class TrackerApplicationAutoMapperProfile : Profile
{
    public TrackerApplicationAutoMapperProfile()
    { 
        CreateMap<Material, MaterialDto>().Ignore(p=>p.TenantName).Ignore(p=>p.MaterialTypeName);
        CreateMap<CreateOrUpdateMaterialDto, Material>();
 
        CreateMap<MaterialType, MaterialTypeDto>();
        CreateMap<CreateOrUpdateMaterialTypeDto, MaterialType>();
 
        CreateMap<Checkin, CheckinDto>();
        CreateMap<CreateOrUpdateCheckinDto, Checkin>();
 
        CreateMap<MaintenanceReference, MaintenanceReferenceDto>();
        CreateMap<CreateOrUpdateMaintenanceReferenceDto, MaintenanceReference>();

        CreateMap<MaintenanceEntity, MaintenanceEntityDto>(); 
        CreateMap<Maintenance, MaintenanceDto>().Ignore(p=>p.Entities);

        CreateMap<UpdateMaintenanceDto, Maintenance>();
        
 
        CreateMap<Anomaly, AnomalyDto>();
        CreateMap<CreateAnomalyDto, Anomaly>();
        CreateMap<UpdateAnomalyDto, Anomaly>();

        CreateMap<MyTenantCreateDto, TenantCreateDto>().ReverseMap().Ignore(p => p.City).Ignore(p => p.Country);
        CreateMap<MyTenantUpdateDto, TenantUpdateDto>().ReverseMap().Ignore(p => p.City).Ignore(p => p.Country);
    }
}
