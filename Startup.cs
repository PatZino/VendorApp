using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDbSpCallMVC.Models.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreDbSpCallMVC
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
            services.AddControllersWithViews();
            // Register SQL database configuration context as services.    
            services.AddDbContext<db_core_sp_callContext>(options => options.UseSqlServer(Configuration.GetConnectionString("db_core_ef_first")));
			
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Vendor}/{action=Index}/{id?}");
            });
        }
    }
}
