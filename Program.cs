using Brickwell.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetEscapades.AspNetCore.SecurityHeaders;
using Brickwell.CustomMiddleware;
using Brickwell.Components;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var services = builder.Services;

		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();

        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential 
            // cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.ConsentCookieValue = "true";
        });

		services.AddDbContext<BrickDbContext>(options =>
			options.UseSqlServer(connectionString));

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
			app.UseDeveloperExceptionPage();
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

		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");
		app.MapRazorPages();

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

//		var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<IBrickRepository, EFBrickRepository>();
        services.AddTransient<ColorViewComponent>();
        using(var scope = app.Services.CreateScope()) 
        {
            var roleManager = 
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "Manager", "Member"}; // Maybe add jsut Admin and Customer

            foreach (var role in roles) 
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

//		services.AddScoped<IBrickRepository, EFBrickRepository>();
//		services.AddTransient<ColorViewComponent>();

        using (var scope = app.Services.CreateScope())
        {
            var userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
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

        app.MapRazorPages();

        app.Run();
    }

}
