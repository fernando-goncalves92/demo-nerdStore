using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStore.WebApp.MVC.Facilities;
using NerdStore.WebApp.MVC.Middlewares;

namespace NerdStore.WebApp.MVC.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<UrlAccess>(configuration);

            return services;
        }

        public static IApplicationBuilder UseMvcConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/error/500");
            //    app.UseStatusCodePagesWithRedirects("/error/{0}");
            //    app.UseHsts();
            //}

            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityConfig();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalog}/{action=Index}/{id?}");
            });

            return app;
        }
    }
}
