using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Horus.Repositories.Interface;
using Horus.Repositories.Implementation;
using Horus.Data;
using Horus.Models;

namespace Horus
{
    public class Startup
    {
        private string ConnectionString;
        public Startup(IHostingEnvironment envApp)
        {
            ConnectionString = $@"Data Source={envApp.ContentRootPath}/users.db";
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite(ConnectionString));

            services.AddIdentity<UserApplication, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddAuthentication();

            services.AddTransient<IParentRepository, ParentRepository>();
            services.AddTransient<IChildRepository, ChildRepository>();
            services.AddTransient<IAlertRepository, AlertRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseIdentity();

            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                        "API",
                        "{controller}/{action}/{id?}");
                });
        }
    }
}
