using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftwareApplicationManager.BL;
using SoftwareApplicationManager.DAL;
using SoftwareApplicationManager.DAL.EF;

namespace SoftwareApplicationManager.UI.MVC
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
            // Dependency injection.
            services.AddDbContext<SoftwareApplicationManagerContext>(ServiceLifetime.Scoped);
            //services.AddScoped<DbContext, SoftwareApplicationManagerContext>();
            //services.AddDbContext<SoftwareApplicationManagerContext>();
            
            //Controller will be able to use the repository and manager.
            services.AddScoped<IRepository, SoftwareApplicationRepository>();
            services.AddScoped<IManager, ApplicationManager>();

            services.AddControllersWithViews();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            
            // Web API print result as XML.
            //services.AddControllers().AddXmlSerializerFormatters();
            
            services.AddCors(options => options.AddDefaultPolicy(builder =>
                builder.WithOrigins("https://localhost:5051")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            ));

        } // ConfigureServices.

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
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}