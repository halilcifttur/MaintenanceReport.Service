using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace TrackerApp;

[Dependency(ReplaceServices = true)]
public class TrackerAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TrackerApp";
}
