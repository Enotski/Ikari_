using Ikari.Data.Abstraction;
using Ikari.Data.Abstraction.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Ikari {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // аутентификация по куки
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = new PathString("/UserProfile/Login");
                    options.LogoutPath = new PathString("/UserProfile/Logout");
                });
            builder.Services.AddAuthorization();

            // mvc (компиляция razor в рантайме, для удобства)
            builder.Services.AddControllersWithViews().AddJsonOptions(options => {
                options.JsonSerializerOptions.WriteIndented = true;
            }).AddRazorRuntimeCompilation();

            // контекст бд
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDbContext<IkariDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // для IIS
            builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());
            builder.WebHost.UseIISIntegration();

            var app = builder.Build();
            
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=GetPage}/{id?}");
            app.Run();
        }
    }
}