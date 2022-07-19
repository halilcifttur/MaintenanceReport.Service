using TrackerApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TrackerApp.Permissions;

public class TrackerAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TrackerAppPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TrackerAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TrackerAppResource>(name);
    }
}
