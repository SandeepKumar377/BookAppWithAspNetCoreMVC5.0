using DiverseBookApp.Data;
using DiverseBookApp.Helpers;
using DiverseBookApp.Models;
using DiverseBookApp.Repository;
using DiverseBookApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiverseBookApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookAppContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultString")));
            services.AddIdentity<ApplicationUsers, IdentityRole>().AddEntityFrameworkStores<BookAppContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
            });
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = _configuration["Application:LoginPath"];
            });

            services.AddControllersWithViews();
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUsers>, ApplicationUserClaimsPrincipalFactory>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(
                //    name: "Default",
                //    pattern: "bookapp/{controller=Home}/{action=Index}/{id}");
            });
        }
    }
}
