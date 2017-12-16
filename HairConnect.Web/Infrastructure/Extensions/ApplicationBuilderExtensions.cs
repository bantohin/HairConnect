namespace HairConnect.Web.Infrastructure.Extensions
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<HairConnectDbContext>().Database.Migrate();

                UserManager<User> userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                    {
                        string adminName = WebConstants.AdminRole;

                        string[] roles = new string[]
                        {
                            adminName,
                            WebConstants.HairdresserRole
                        };

                        foreach (string role in roles)
                        {
                            bool roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        string adminEmail = "admin@admin.com";
                        User admin = await userManager.FindByEmailAsync(adminEmail);

                        if (admin == null)
                        {
                            admin = new User()
                            {
                                FirstName = adminName,
                                UserName = adminEmail,
                                LastName = adminName,
                                Email = adminEmail,
                                PhoneNumber = adminName,
                                SecurityStamp = Guid.NewGuid().ToString()
                            };

                            await userManager.CreateAsync(admin, "admin12");

                            await userManager.AddToRoleAsync(admin, adminName);
                        }
                    }).Wait();
            }

            return app;
        }
    }
}
