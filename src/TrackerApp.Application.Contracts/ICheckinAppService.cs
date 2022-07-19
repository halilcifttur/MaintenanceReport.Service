using System;
using TrackerApp.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TrackerApp;

public interface ICheckinAppService : ICrudAppService<CheckinDto,Guid, PagedAndSortedResultRequestDto, CreateOrUpdateCheckinDto>
{
    
}