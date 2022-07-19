using System;
using System.Collections.Generic;
using System.Text;
using TrackerApp.Localization;
using Volo.Abp.Application.Services;

namespace TrackerApp;

/* Inherit your application services from this class.
 */
public abstract class TrackerAppService : ApplicationService
{
    protected TrackerAppService()
    {
        LocalizationResource = typeof(TrackerAppResource);
    }
}
