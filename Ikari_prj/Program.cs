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

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = new PathString("/UserProfile/Login");
                    options.LogoutPath = new PathString("/UserProfile/Logout");
                });
            builder.Services.AddAuthorization();

            builder.Services.AddControllersWithViews().AddJsonOptions(options => {
                options.JsonSerializerOptions.WriteIndented = true;
            });
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDbContext<IkariDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

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