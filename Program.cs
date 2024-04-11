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

        var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
        builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
        var services = builder.Services;
        var configuration = builder.Configuration;



        services.AddAuthentication().AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["Authentication__Google__ClientId"] ?? "DefaultClientId";
            googleOptions.ClientSecret = configuration["Authentication__Google__ClientSecret"] ?? "DefaultClientSecret";

        });

        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential 
            // cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.ConsentCookieValue = "true";
        });

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<BrickDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<BrickDbContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IBrickRepository, EFBrickRepository>();
        builder.Services.AddTransient<ColorViewComponent>();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 12;
    options.Password.RequiredUniqueChars = 1;
});



builder.Services.AddScoped<IBrickRepository, EFBrickRepository>();
builder.Services.AddTransient<ColorViewComponent>();

        builder.Services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
        });

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


var app = builder.Build();



        app.UseMiddleware<ContentSecurityPolicyMiddleware>();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();

        app.Use(async (context, next) =>
        {
        context.Response.Headers.Add("X - Content - Type - Options", "nosniff");
        context.Response.Headers.Add("X - XSS - Protection", "1; mode = block");
        context.Response.Headers.Add("Referrer - Policy", "no - referrer");
        // Define your Content-Security-Policy
        string csp = "default - src ‘self’; " +
         "script - src ‘self’ ‘unsafe-inline’; " +
         "style - src ‘self’ ‘unsafe-inline’ https://fonts.googleapis.com; " + // For Google Fonts
         "img - src ‘self’ data: https://m.media-amazon.com https://www.lego.com https://images.brickset.com https://www.brickeconomy.com; " + // Domains for images
         "font - src ‘self’ https://fonts.gstatic.com;"; // For Google Fonts
                                  // Add Content-Security-Policy without overwriting existing headers
        if (!context.Response.Headers.ContainsKey("Content - Security - Policy"))
        {
            context.Response.Headers.Add("Content - Security - Policy", csp);
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
}



