﻿using Orchard.Security;
using Orchard.UI.Navigation;

namespace Orchard.Users
{
    public class AdminMenu : INavigationProvider
    {
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("users")
                .Add("Users", "11",
                    menu => menu.Action("Index", "Admin", new { area = "Orchard.Users" })
                        .Add("Users", "1.0", item => item.Action("Index", "Admin", new { area = "Orchard.Users" })
                            .LocalNav().Permission(Permissions.ManageUsers)));
        }
    }
}
