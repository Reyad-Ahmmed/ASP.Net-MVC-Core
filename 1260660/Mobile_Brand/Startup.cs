using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mobile_Brand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_Brand
{
    public class Startup
    {
        public Startup (IConfiguration config) { this.Config = config; }
        public IConfiguration Config { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MobileDbContext>(o => o.UseSqlServer(this.Config.GetConnectionString("mstring")));
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(this.Config.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<AppUser>().AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AppDbContext>();

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opt =>
             {
                 opt.LoginPath = "/Account/Login";
             });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
