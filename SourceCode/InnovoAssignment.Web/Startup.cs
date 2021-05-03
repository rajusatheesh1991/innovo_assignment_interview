using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using InnovoAssignment.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InnovoAssignment.Web
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
            services.AddSession(options =>
            {
                options.Cookie.Name = ".UserManage.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

       //     var options=  new SqlServerStorageOptions

       //     {
       //         CommandBatchMaxTimeout = TimeSpan.FromMinutes(15),
       //         SlidingInvisibilityTimeout = TimeSpan.FromMinutes(15),
       //         QueuePollInterval = TimeSpan.Zero,
       //         UseRecommendedIsolationLevel = true,
       //         DisableGlobalLocks = true
       //     };
       //     JobStorage.Current = new SqlServerStorage("HangfireConnection", options);
       //     services.AddHangfire(configuration => configuration
       // .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       // .UseSimpleAssemblyNameTypeSerializer()
       // .UseRecommendedSerializerSettings()
       // .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"),
       //options));
            
       //     // Add the processing server as IHostedService
       //     services.AddHangfireServer();


            services.AddWebServices();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new HangfireAuthorizationFilter() }
            //});
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Customer}/{action=Login}/{id?}");
            });
        }
    }
}
