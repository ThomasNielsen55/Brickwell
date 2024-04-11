using Brickwell.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetEscapades.AspNetCore.SecurityHeaders;
using Brickwell.CustomMiddleware;
using Brickwell.Components;
using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.Scripting;
using System;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();

        // Add Content Security Policy middleware directly here
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");

            string csp = "default-src 'self'; " +
                         "script-src 'self' 'unsafe-inline'; " +
                         "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
                         "img-src 'self' data: https://m.media-amazon.com https://www.lego.com https://images.brickset.com https://www.brickeconomy.com; " +
                         "font-src 'self' https://fonts.gstatic.com;";

            if (!context.Response.Headers.ContainsKey("Content-Security-Policy"))
            {
                context.Response.Headers.Add("Content-Security-Policy", csp);
            }

            await next();
        });

        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Add your service registrations here
        services.AddDbContext<BrickDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options =>
            options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<BrickDbContext>();

        services.AddControllersWithViews();

        services.AddScoped<IBrickRepository, EFBrickRepository>();
        services.AddTransient<ColorViewComponent>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 12;
            options.Password.RequiredUniqueChars = 1;
        });

        services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
        });

        services.AddDistributedMemoryCache();
        services.AddSession();
    }
}




