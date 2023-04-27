using Microsoft.AspNetCore.Identity;
using risk.control.system.Models;

namespace risk.control.system.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.PortalAdmin.ToString(), AppRoles.PortalAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAdmin.ToString(), AppRoles.ClientAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientCreator.ToString(), AppRoles.ClientCreator.ToString()));
        }
    }
}