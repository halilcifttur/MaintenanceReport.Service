using System;
using System.Threading.Tasks;
using TrackerApp.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TrackerApp;

public interface IMaterialAppService : ICrudAppService<MaterialDto,Guid, PagedAndSortedResultRequestDto, CreateOrUpdateMaterialDto>
{
    
}