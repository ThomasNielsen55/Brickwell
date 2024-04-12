using Brickwell.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetEscapades.AspNetCore.SecurityHeaders;
using Brickwell.CustomMiddleware;
using Brickwell.Components;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;


        var googleClientId = builder.Configuration["Authentication__Google__ClientId"];
        var googleClientSecret = builder.Configuration["Authentication__Google__ClientSecret"];
        Console.WriteLine($"Google Client ID: {googleClientId}");
        Console.WriteLine($"Google Client Secret: {googleClientSecret}");
        builder.Services.AddAuthentication()
          .AddGoogle(options =>
          {
              options.ClientId = googleClientId;
              options.ClientSecret = googleClientSecret;
          });

        //services.AddAuthentication().AddGoogle(googleOptions =>
        //{
        //    googleOptions.ClientId = configuration["Authentication__Google__ClientId"];
        //    googleOptions.ClientSecret = configuration["Authentication__Google__ClientSecret"];
        //});

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

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<BrickDbContext>();
        builder.Services.AddControllersWithViews();

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

        builder.Services.AddRazorPages();

        builder.Services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
        });


        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession();


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
             "img-src 'self' 'unsafe-inline' data: " +
             "https://m.media-amazon.com " +
             "https://www.lego.com " +
             "https://images.brickset.com " +
             "https://www.brickeconomy.com; " +  // Added image sources here
             "font-src 'self' https://fonts.gstatic.com;";


            if (!context.Response.Headers.ContainsKey("Content-Security-Policy"))
            {
                context.Response.Headers.Add("Content-Security-Policy", csp);
            }

            await next();
        });

        app.UseSession();

        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");


        using (var scope = app.Services.CreateScope())
        {
            var roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "Customer" }; // Maybe add jsut Admin and Customer

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        using (var scope = app.Services.CreateScope())
        {
            var userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();



            app.MapRazorPages();

            app.Run();
        }
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





        services.AddDistributedMemoryCache();
        services.AddSession();
    }
}