using TrackerApp.Localization;
using Volo.Abp.AspNetCore.Components;

namespace TrackerApp.Blazor;

public abstract class TrackerAppComponentBase : AbpComponentBase
{
    protected TrackerAppComponentBase()
    {
        LocalizationResource = typeof(TrackerAppResource);
    }
}
