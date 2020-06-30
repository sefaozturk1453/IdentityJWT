using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityJWT.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityJWT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<AppIdentityDbContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("SefaConnection"))); ///appsettings.json dosyasında tanımladığımız bağlantı ekleniyor.

            services.AddIdentity<ApplicationUser, IdentityRole>()  //Eklendi
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();






            //----------------------------------------
            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(Options =>
                {
                    Options.SaveToken = true;
                    Options.RequireHttpsMetadata = true;
                    Options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://localhost:44393",
                        ValidateAudience = true,
                        ValidAudience = "https://localhost:44393",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UzuncaBirSifreee"))
                    };
                });  
            //--------------------------------
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            SeedDate.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider).Wait(); //SeedDate i kullanması için tanımladık -- asenkron olarak çağırdığımız için. ".wait()" sonuna ekledik.

            app.UseAuthentication();// Authentication kullanıcağımızı belirtiyoruz
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
