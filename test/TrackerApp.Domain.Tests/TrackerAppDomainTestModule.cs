using TrackerApp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TrackerApp;

[DependsOn(
    typeof(TrackerAppEntityFrameworkCoreTestModule)
    )]
public class TrackerAppDomainTestModule : AbpModule
{

}
