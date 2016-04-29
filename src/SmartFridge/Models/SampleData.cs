using System;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartFridge.Models {
    public class SampleData {
        public async static Task Initialize(IServiceProvider serviceProvider) {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            context.Database.Migrate();

            // Add MOND as administrators.
            var mason = await userManager.FindByNameAsync("mwilliam09@gmail.com");
            if(mason == null) {
                // create user
                mason = new ApplicationUser {
                    UserName = "mwilliam09@gmail.com",
                    Email = "mwilliam09@gmail.com"
                };
                await userManager.CreateAsync(mason, "Coder: Camps7!");

                // add claims
                await userManager.AddClaimAsync(mason, new Claim("IsAdmin", "true"));
            }

            // Ensure Stephen (IsAdmin)
            var stephen = await userManager.FindByNameAsync("Stephen.Walther@CoderCamps.com");
            if(stephen == null) {
                // create user
                stephen = new ApplicationUser {
                    UserName = "Stephen.Walther@CoderCamps.com",
                    Email = "Stephen.Walther@CoderCamps.com"
                };
                await userManager.CreateAsync(stephen, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(stephen, new Claim("IsAdmin", "true"));
            }

            // Ensure Mike (not IsAdmin)
            var mike = await userManager.FindByNameAsync("Mike@CoderCamps.com");
            if(mike == null) {
                // create user
                mike = new ApplicationUser {
                    UserName = "Mike@CoderCamps.com",
                    Email = "Mike@CoderCamps.com"
                };
                await userManager.CreateAsync(mike, "Secret123!");
            }


        }

    }
}
