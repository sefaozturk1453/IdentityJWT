using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityJWT.Data
{
    public class SeedDate
    {
        public static async Task Initialize(IServiceProvider serviceProvider) //Static bir method ekledik, asenkron olarak
        {
            var context = serviceProvider.GetRequiredService<AppIdentityDbContext>(); //DbContext imizden bir context oluşturuyoruz. DbContextimizi context e atadık
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>(); //User Yönetmek için userManager a ApplicationUser ı yolluyoruz

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string rolAdmin = "admin";
            string rolEditor = "editor";

            context.Database.EnsureCreated();//Veritabanımız yoksa oluşmasını sağlıyoruz.


            ApplicationUser user = new ApplicationUser()
            {
                UserName = "Sefa",
                Email = "sefa@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };


            if (!context.Users.Any()) //Database de users tablosu boş ise
            {          

                await userManager.CreateAsync(user, "@pASSWORD125"); //Oluşturduğumuz user nesnesini ekliyoruz.


                if (!context.Roles.Any())
                {
                    if (await roleManager.FindByNameAsync(rolAdmin) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(rolAdmin));
                    }
                    if (await roleManager.FindByNameAsync(rolEditor) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(rolEditor));

                    }
                }

                await userManager.AddToRoleAsync(user, rolAdmin);
            }


        }
    }
}
