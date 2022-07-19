using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TrackerApp.Data;

/* This is used if database provider does't define
 * ITrackerAppDbSchemaMigrator implementation.
 */
public class NullTrackerAppDbSchemaMigrator : ITrackerAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
