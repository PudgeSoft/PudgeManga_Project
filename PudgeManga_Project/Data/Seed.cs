﻿//using Microsoft.AspNetCore.Identity;
//using PudgeManga_Project.Models;

//namespace PudgeManga_Project.Data
//{
//    public class Seed
//    {
//        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
//        {
//            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//            {
//                //Roles
//                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
//                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
//                if (!await roleManager.RoleExistsAsync(UserRoles.User))
//                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

//                //Users
//                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
//                string adminUserEmail = "Z";

//                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
//                if (adminUser == null)
//                {
//                    var newAdminUser = new User()
//                    {
//                        UserName = "teddysmithdev",
//                        Email = adminUserEmail,
//                        EmailConfirmed = true,
//                    };
//                    await userManager.CreateAsync(newAdminUser, "SuperUserZVVMTAO?");
//                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
//                }

//                string appUserEmail = "user@etickets.com";

//                var appUser = await userManager.FindByEmailAsync(appUserEmail);
//                if (appUser == null)
//                {
//                    var newAppUser = new User()
//                    {
//                        UserName = "app-user",
//                        Email = appUserEmail,
//                        EmailConfirmed = true,

//                    };
//                    await userManager.CreateAsync(newAppUser, "SuperUserZVVMTAO?");
//                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
//                }
//            }
//        }
//    }
//}