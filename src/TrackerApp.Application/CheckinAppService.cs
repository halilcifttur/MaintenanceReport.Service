using System;
using System.Threading.Tasks;
using TrackerApp.Dtos;
using TrackerApp.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TrackerApp;

public class CheckinAppService : CrudAppService<Checkin, CheckinDto, Guid, PagedAndSortedResultRequestDto, CreateOrUpdateCheckinDto>, ICheckinAppService
{
    public CheckinAppService(IRepository<Checkin, Guid> repository) : base(repository)
    {
    }
}