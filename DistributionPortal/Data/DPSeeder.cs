using DistributionPortal.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributionPortal.Data
{
    public class DPSeeder
    {
        private readonly DPDBContext ctx;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<UserDetails> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DPSeeder(DPDBContext ctx, IHostingEnvironment hosting, UserManager<UserDetails> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.ctx = ctx;
            this.hosting = hosting;
            this.userManager = userManager;
            this.roleManager = roleManager;

        }

        public async Task Seed()
        {
            ctx.Database.EnsureCreated();

            try
            {
                //create role
                string id = "1";
                var result_ex = await roleManager.FindByIdAsync(id);

                if (result_ex == null)
                {
                    var role = new Microsoft.AspNetCore.Identity.IdentityRole();
                    role.Id = id;
                    role.Name = "KM Admin";
                    role.NormalizedName = "KMADMIN";
                    var result = await roleManager.CreateAsync(role);

                }

                //create user
                UserDetails user = null;
                var result_ex2 = await userManager.FindByEmailAsync("superadmin@gmail.com");
                if (result_ex2 == null)
                {
                    user = new UserDetails()
                    {
                        UserName = "superadmin",
                        Email = "superadmin@gmail.com",
                        PhoneNumber = "0717686411",
                        UserStatus = true,
                        Notifications = "True|True",
                        CreatedBy = "Auto",
                        CreatedTime = DateTime.Now
                    };
                    var result2 = await userManager.CreateAsync(user, "Admin@1234");
                    if (result2 != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Falied to create default user");
                    }

                    if (result2.Succeeded)
                    {
                        var role2 = roleManager.FindByIdAsync(id);
                        var roleResult = await userManager.AddToRoleAsync(user, role2.Result.Name);
                    }
                }

                ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                
            }

        }
    }
}
