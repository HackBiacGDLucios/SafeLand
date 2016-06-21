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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services, IHostingEnvironment envApp)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite($@"Data Source={envApp.ContentRootPath}/users.db"));

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
