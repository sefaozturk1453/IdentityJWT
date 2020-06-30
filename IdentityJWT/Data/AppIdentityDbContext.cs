using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityJWT.Data
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser> //Identity ı kullanmak için implement(Uygulamaya dahil etmek) ediyoruz.<ApplicationUser> generate ediyoruz ":IdentityDbContext" 
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) //override void OnModelCreating "Çift tık"
        {
            base.OnModelCreating(builder);
        }
    }
}
