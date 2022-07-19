using TrackerApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TrackerApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TrackerAppController : AbpControllerBase
{
    protected TrackerAppController()
    {
        LocalizationResource = typeof(TrackerAppResource);
    }
}
