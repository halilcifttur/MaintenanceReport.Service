using TrackerApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace TrackerApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TrackerAppEntityFrameworkCoreModule),
    typeof(TrackerAppApplicationContractsModule)
    )]
public class TrackerAppDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
