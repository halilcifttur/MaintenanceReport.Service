using Volo.Abp.Modularity;

namespace TrackerApp;

[DependsOn(
    typeof(TrackerApplicationModule),
    typeof(TrackerAppDomainTestModule)
    )]
public class TrackerAppApplicationTestModule : AbpModule
{

}
