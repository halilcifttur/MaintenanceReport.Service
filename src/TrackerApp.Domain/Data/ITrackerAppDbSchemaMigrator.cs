using System.Threading.Tasks;

namespace TrackerApp.Data;

public interface ITrackerAppDbSchemaMigrator
{
    Task MigrateAsync();
}
