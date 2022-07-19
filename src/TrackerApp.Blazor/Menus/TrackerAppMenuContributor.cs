using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackerApp.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace TrackerApp.Blazor.Menus;

public class TrackerAppMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public TrackerAppMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<TrackerAppResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                TrackerAppMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "TrackerApp",
                l["Menu:Materials"],
                icon: "fa fa-book"
            ).AddItem(
                new ApplicationMenuItem(
                    "TrackerApp.Materials",
                    l["Menu:Materials"],
                    url: "/materials"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    "TrackerApp.Checkin",
                    l["Menu:Checkin"],
                    url: "/check-types"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    "TrackerApp.MaterialTypes",
                    l["Menu:MaterialTypes"],
                    url: "/material-types"
                )
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "TrackerApp",
                l["Menu:MaintenanceReference"],
                icon: "fa fa-book"
            ).AddItem(
                new ApplicationMenuItem(
                    "TrackerApp.MaintenanceReferences",
                    l["Menu:MaintenanceReferences"],
                    url: "/maintenance-references"
                )
            )
        );
        
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "TrackerApp",
                l["Menu:Maintenance"],
                icon: "fa fa-book"
            ).AddItem(
                new ApplicationMenuItem(
                    "TrackerApp.Maintenance",
                    l["Menu:Maintenance"],
                    url: "/maintenance"
                )
            )
        );
        
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "TrackerApp",
                l["Menu:Anomaly"],
                icon: "fa fa-book"
            ).AddItem(
                new ApplicationMenuItem(
                    "TrackerApp.Anomaly",
                    l["Menu:Anomaly"],
                    url: "/anomaly"
                )
            )
        );

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());
        
        return Task.CompletedTask;
    }
}
