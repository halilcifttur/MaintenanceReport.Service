using System;
using TrackerApp.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TrackerApp;

public interface IAnomalyAppService : ICrudAppService<AnomalyDto,Guid,PagedAnomalyDto,CreateAnomalyDto, UpdateAnomalyDto>
{
    
}