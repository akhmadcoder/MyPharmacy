using Microsoft.AspNetCore.Identity;
using MyPharmacy.Data.Static;
using MyPharmacy.Models;

namespace MyPharmacy.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                // Categories
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Antibiotics"
                        },
                        new Category()
                        {
                            Name = "Antifungals"
                        },
                        new Category()
                        {
                            Name = "Cold Cures"
                        },
                        new Category()
                        {
                            Name = "Cough Suppressants"
                        },
                        new Category()
                        {
                            Name = "Hormones"
                        },
                        new Category()
                        {
                            Name = "Vitamins"
                        },
                        new Category()
                        {
                            Name = "Sleeping Drugs"
                        },
                    });
                    context.SaveChanges();

                }

                // Countries
                if (!context.Countries.Any())
                {
                    context.Countries.AddRange(new List<Country>()
                    {
                        new Country()
                        {
                            Name = "Uzbekistan"
                        },
                        new Country()
                        {
                            Name = "India"
                        },
                        new Country()
                        {
                            Name = "Kazakstan"
                        },
                        new Country()
                        {
                            Name = "Turkey"
                        },
                        new Country()
                        {
                            Name = "Russia"
                        },
                        new Country()
                        {
                            Name = "Poland"
                        },
                        new Country()
                        {
                            Name = "Germany"
                        },
                    });

                    context.SaveChanges();
                }

                // Clients
                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(new List<Client>()
                    {
                        new Client()
                        {
                            Name = "NATA PHARM DORIXONASI",
                            Email = "nata_ph@gmail.com",
                            Phone = "724-91-67",
                            Address = "Yunusobod, AHMAD DONISH ko'chasi, YUNUSOBOD-11 mavzesi, 52 uy"
                        },
                        new Client()
                        {
                            Name = "SUNITA FARM DORIXONASI",
                            Email = "sunita_ph@gmail.com",
                            Phone = "223-32-05",
                            Address = "YANGI YUNUSOBOD ko'chasi, YUNUSOBOD-13 mavzesi, 58/1 uy"
                        },
                        new Client()
                        {
                            Name = "GO DORIXONA",
                            Email = "go_phar@gmail.com",
                            Phone = "150-44-44",
                            Address = "Mirzo-Ulug'bek, ARSLANOBOD ko'chasi, 4 uy"
                        },
                        new Client()
                        {
                            Name = "BIORITM-INVEST",
                            Email = "bioritm_ph@gmail.com",
                            Phone = "261-38-61",
                            Address = "Mirzo-Ulug'bek, BUYUK IPAK YO'LI ko'chasi, 428 uy"
                        },
                        new Client()
                        {
                            Name = "OXYMED",
                            Email = "oxymed_ph@gmail.com",
                            Phone = "200-03-03",
                            Address = "Sirg'ali, BEZAKCHILIK ko'chasi, 18A uy"
                        },

                    });
                    context.SaveChanges();
                }

            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(newAdminUser, "Admin123?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

            }
        }
    }
}
