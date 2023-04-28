using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using risk.control.system.Data;
using risk.control.system.Helpers;
using risk.control.system.Models;

namespace risk.control.system.Seeds
{
    public static class DatabaseSeed
    {
        public static async Task SeedDatabase(WebApplication app) //can be placed at the very bottom under app.Run()
        {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            context.Database.EnsureCreated();

            //check for users
            if (context.ApplicationUser.Any())
            {
                return; //if user is not empty, DB has been seed
            }

            //CREATE ROLES
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.PortalAdmin.ToString().Substring(0, 2).ToUpper(), AppRoles.PortalAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAdmin.ToString().Substring(0, 2).ToUpper(), AppRoles.ClientAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.VendorAdmin.ToString().Substring(0, 2).ToUpper(), AppRoles.VendorAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientCreator.ToString().Substring(0, 2).ToUpper(), AppRoles.ClientCreator.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAssigner.ToString().Substring(0, 2).ToUpper(), AppRoles.ClientAssigner.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAssessor.ToString().Substring(0, 2).ToUpper(), AppRoles.ClientAssessor.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.VendorSupervisor.ToString().Substring(0, 2).ToUpper(), AppRoles.VendorSupervisor.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.VendorAgent.ToString().Substring(0, 2).ToUpper(), AppRoles.VendorAgent.ToString()));

            #region COUNTRY STATE PINCODE
            var india = new Country
            {
                Name = "INDIA",
                Code = "IND",
            };
            var indiaCountry = await context.Country.AddAsync(india);
            var australia = new Country
            {
                Name = "AUSTRALIA",
                Code = "AUS",
            };

            var australiaCountry = await context.Country.AddAsync(australia);
            var canada = new Country
            {
                Name = "CANADA",
                Code = "CAN",
            };

            var canadaCountry = await context.Country.AddAsync(canada);
            var up = new State
            {
                CountryId = indiaCountry.Entity.CountryId,
                Name = "UTTAR PRADESH",
                Code = "UP"
            };

            var upState = await context.State.AddAsync(up);
            var ontario = new State
            {
                CountryId = canadaCountry.Entity.CountryId,
                Name = "ONTARIO",
                Code = "ON"
            };

            var ontarioState = await context.State.AddAsync(ontario);

            var delhi = new State
            {
                CountryId = indiaCountry.Entity.CountryId,
                Name = "NEW DELHI",
                Code = "NDL"
            };

            var delhiState = await context.State.AddAsync(delhi);

            var victoria = new State
            {
                CountryId = australiaCountry.Entity.CountryId,
                Name = "VICTORIA",
                Code = "VIC"
            };

            var victoriaState = await context.State.AddAsync(victoria);

            var tasmania = new State
            {
                CountryId = australiaCountry.Entity.CountryId,
                Name = "TASMANIA",
                Code = "TAS"
            };

            var tasmaniaState = await context.State.AddAsync(tasmania);

            var newDelhi = new PinCode
            {
                Name = "NEW DELHI",
                Code = "110001",
                State = delhiState.Entity,
                Country = indiaCountry.Entity
            };

            var newDelhiPinCode = await context.PinCode.AddAsync(newDelhi);

            var northDelhi = new PinCode
            {
                Name = "NORTH DELHI",
                Code = "110002",
                State = delhiState.Entity,
                Country = indiaCountry.Entity
            };

            var northDelhiPinCode = await context.PinCode.AddAsync(northDelhi);

            var indirapuram = new PinCode
            {
                Name = "INDIRAPURAM",
                Code = "201014",
                State = upState.Entity,
                Country = indiaCountry.Entity
            };

            var indiraPuramPinCode = await context.PinCode.AddAsync(indirapuram);

            var bhelupur = new PinCode
            {
                Name = "BHELUPUR",
                Code = "221001",
                State = upState.Entity,
                Country = indiaCountry.Entity
            };

            var bhelupurPinCode = await context.PinCode.AddAsync(bhelupur);

            var forestHill = new PinCode
            {
                Name = "FOREST HILL",
                Code = "3131",
                State = victoriaState.Entity,
                Country = australiaCountry.Entity
            };

            var forestHillPinCode = await context.PinCode.AddAsync(forestHill);

            var vermont = new PinCode
            {
                Name = "VERMONT",
                Code = "3133",
                State = victoriaState.Entity,
                Country = australiaCountry.Entity
            };

            var vermontPinCode = await context.PinCode.AddAsync(vermont);

            var tasmaniaCity = new PinCode
            {
                Name = "TASMANIA CITY",
                Code = "7000",
                State = tasmaniaState.Entity,
                Country = australiaCountry.Entity
            };

            var tasmaniaCityCode = await context.PinCode.AddAsync(tasmaniaCity);

            var torontoCity = new PinCode
            {
                Name = "TORONTO",
                Code = "9101",
                State = ontarioState.Entity,
                Country = canadaCountry.Entity
            };

            var torontoCityCode = await context.PinCode.AddAsync(tasmaniaCity);

            #endregion

            await InvestigationServiceSeed.Seed(context);

            #region APPLICATION USERS ROLES

            //CREATE CLIENT COMPANY

            var TataAig = new ClientCompany
            {
                ClientCompanyId = Guid.NewGuid().ToString(),
                Name = "TATA AIG INSURANCE",
                Addressline = "100 GOOD STREET ",
                Branch = "FOREST HILL CHASE",
                City = "FOREST HILL",
                Code = "TA001",
                Country = australiaCountry.Entity,
                CountryId = australiaCountry.Entity.CountryId,
                State = victoriaState.Entity,
                StateId = victoriaState.Entity.StateId,
                PinCode = forestHillPinCode.Entity,
                PinCodeId = forestHillPinCode.Entity.PinCodeId,
                Description = "HEAD OFFICE ",
                Email = "tata-aig@mail.com",
                PhoneNumber = "(03) 88004739",
            };

            var tataAigCompany = await context.ClientCompany.AddAsync(TataAig);

            await context.SaveChangesAsync();

            //Seed portal admin
            var portalAdmin = new ApplicationUser()
            {
                UserName = "portal-admin@admin.com",
                Email = "portal-admin@admin.com",
                FirstName = "Portal",
                LastName = "Admin",
                Password = ApplicationUserOptions.Password,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                State = upState.Entity,
                Country = indiaCountry.Entity,
                PinCode = indiraPuramPinCode.Entity,
                ProfilePictureUrl = "img/superadmin.jpg"
            };
            if (userManager.Users.All(u => u.Id != portalAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(portalAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(portalAdmin, ApplicationUserOptions.Password);
                    await userManager.AddToRoleAsync(portalAdmin, AppRoles.PortalAdmin.ToString());
                    await userManager.AddToRoleAsync(portalAdmin, AppRoles.ClientAdmin.ToString());
                    await userManager.AddToRoleAsync(portalAdmin, AppRoles.ClientCreator.ToString());
                    await userManager.AddToRoleAsync(portalAdmin, AppRoles.ClientAssigner.ToString());
                    await userManager.AddToRoleAsync(portalAdmin, AppRoles.ClientAssessor.ToString());
                    await userManager.AddToRoleAsync(portalAdmin, AppRoles.VendorAdmin.ToString());
                    await userManager.AddToRoleAsync(portalAdmin, AppRoles.VendorSupervisor.ToString());
                    await userManager.AddToRoleAsync(portalAdmin, AppRoles.VendorAgent.ToString());
                }
                var adminRole = await roleManager.FindByNameAsync(AppRoles.PortalAdmin.ToString());
                var allClaims = await roleManager.GetClaimsAsync(adminRole);
                var allPermissions = Permissions.GeneratePermissionsForModule("Products");
                foreach (var permission in allPermissions)
                {
                    if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                    {
                        await roleManager.AddClaimAsync(adminRole, new Claim("Permission", permission));
                    }
                }
            }


            //Seed client admin
            var clientAdmin = new ApplicationUser()
            {
                UserName = "client-admin@admin.com",
                Email = "client-admin@admin.com",
                FirstName = "Client",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Password = ApplicationUserOptions.Password,
                isSuperAdmin = true,
                State = ontarioState.Entity,
                Country = canadaCountry.Entity,
                PinCode = torontoCityCode.Entity,
                ProfilePictureUrl = "img/admin.png"
            };
            if (userManager.Users.All(u => u.Id != clientAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(clientAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(clientAdmin, ApplicationUserOptions.Password);
                    await userManager.AddToRoleAsync(clientAdmin, AppRoles.ClientAdmin.ToString());
                    await userManager.AddToRoleAsync(clientAdmin, AppRoles.ClientCreator.ToString());
                    await userManager.AddToRoleAsync(clientAdmin, AppRoles.ClientAssigner.ToString());
                    await userManager.AddToRoleAsync(clientAdmin, AppRoles.ClientAssessor.ToString());
                    await userManager.AddToRoleAsync(clientAdmin, AppRoles.VendorAdmin.ToString());
                    await userManager.AddToRoleAsync(clientAdmin, AppRoles.VendorSupervisor.ToString());
                    await userManager.AddToRoleAsync(clientAdmin, AppRoles.VendorAgent.ToString());
                }
            }

            //Seed client creator
            var clientCreator = new ApplicationUser()
            {
                UserName = "client-creator@admin.com",
                Email = "client-creator@admin.com",
                FirstName = "Client",
                LastName = "Creator",
                EmailConfirmed = true,
                Password = ApplicationUserOptions.Password,
                PhoneNumberConfirmed = true,
                isSuperAdmin = true,
                State = upState.Entity,
                Country = indiaCountry.Entity,
                PinCode = indiraPuramPinCode.Entity,
                ProfilePictureUrl = "img/creator.jpg"
            };
            if (userManager.Users.All(u => u.Id != clientCreator.Id))
            {
                var user = await userManager.FindByEmailAsync(clientCreator.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(clientCreator, ApplicationUserOptions.Password);
                    await userManager.AddToRoleAsync(clientCreator, AppRoles.ClientCreator.ToString());
                }
            }


            //Seed client assigner
            var clientAssigner = new ApplicationUser()
            {
                UserName = "client-assigner@admin.com",
                Email = "client-assigner@admin.com",
                FirstName = "Client",
                LastName = "Assigner",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Password = ApplicationUserOptions.Password,
                isSuperAdmin = true,
                PinCode = northDelhiPinCode.Entity,
                State = delhiState.Entity,
                Country = indiaCountry.Entity,
                ProfilePictureUrl = "img/assigner.png"
            };
            if (userManager.Users.All(u => u.Id != clientAssigner.Id))
            {
                var user = await userManager.FindByEmailAsync(clientAssigner.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(clientAssigner, ApplicationUserOptions.Password);
                    await userManager.AddToRoleAsync(clientAssigner, AppRoles.ClientAssigner.ToString());
                }
            }

            //Seed client assessor
            var clientAssessor = new ApplicationUser()
            {
                UserName = "client-assessor@admin.com",
                Email = "client-assessor@admin.com",
                FirstName = "Client",
                LastName = "Assessor",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Password = ApplicationUserOptions.Password,
                isSuperAdmin = true,
                PinCode = northDelhiPinCode.Entity,
                State = delhiState.Entity,
                Country = indiaCountry.Entity,
                ProfilePictureUrl = "img/assessor.png"
            };
            if (userManager.Users.All(u => u.Id != clientAssessor.Id))
            {
                var user = await userManager.FindByEmailAsync(clientAssessor.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(clientAssessor, ApplicationUserOptions.Password);
                    await userManager.AddToRoleAsync(clientAssessor, AppRoles.ClientAssessor.ToString());
                }
            }


            //Seed Vendor Admin
            var vendorAdmin = new ApplicationUser()
            {
                UserName = "vendor-admin@admin.com",
                Email = "vendor-admin@admin.com",
                FirstName = "Vendor",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Password = ApplicationUserOptions.Password,
                isSuperAdmin = true,
                PinCode = indiraPuramPinCode.Entity,
                State = upState.Entity,
                Country = indiaCountry.Entity,
                ProfilePictureUrl = "img/vendor-admin.png"
            };
            if (userManager.Users.All(u => u.Id != vendorAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(vendorAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(vendorAdmin, ApplicationUserOptions.Password);
                    await userManager.AddToRoleAsync(vendorAdmin, AppRoles.VendorAdmin.ToString());
                    await userManager.AddToRoleAsync(vendorAdmin, AppRoles.VendorSupervisor.ToString());
                    await userManager.AddToRoleAsync(vendorAdmin, AppRoles.VendorAgent.ToString());
                }
            }



            //Seed Vendor Admin
            var vendorSupervisor = new ApplicationUser()
            {
                UserName = "vendor-supervisor@admin.com",
                Email = "vendor-supervisor@admin.com",
                FirstName = "Vendor",
                LastName = "Supervisor",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Password = ApplicationUserOptions.Password,
                isSuperAdmin = true,
                PinCode = indiraPuramPinCode.Entity,
                State = upState.Entity,
                Country = indiaCountry.Entity,
                ProfilePictureUrl = "img/supervisor.png"
            };
            if (userManager.Users.All(u => u.Id != vendorSupervisor.Id))
            {
                var user = await userManager.FindByEmailAsync(vendorSupervisor.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(vendorSupervisor, ApplicationUserOptions.Password);
                    await userManager.AddToRoleAsync(vendorSupervisor, AppRoles.VendorSupervisor.ToString());
                    await userManager.AddToRoleAsync(vendorSupervisor, AppRoles.VendorAgent.ToString());
                }
            }


            //Seed Vendor Admin
            var vendorAgent = new ApplicationUser()
            {
                UserName = "vendor-agent@admin.com",
                Email = "vendor-agent@admin.com",
                FirstName = "Vendor",
                LastName = "Agent",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Password = ApplicationUserOptions.Password,
                isSuperAdmin = true,
                PinCode = indiraPuramPinCode.Entity,
                State = upState.Entity,
                Country = indiaCountry.Entity,
                ProfilePictureUrl = "img/agent.jpg"
            };
            if (userManager.Users.All(u => u.Id != vendorAgent.Id))
            {
                var user = await userManager.FindByEmailAsync(vendorAgent.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(vendorAgent, ApplicationUserOptions.Password);
                    await userManager.AddToRoleAsync(vendorAgent, AppRoles.VendorAgent.ToString());
                }
            }
            #endregion
        }
    }
}
