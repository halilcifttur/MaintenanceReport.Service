using Volo.Abp.Settings;

namespace TrackerApp.Settings;

public class TrackerAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TrackerAppSettings.MySetting1));
    }
}
