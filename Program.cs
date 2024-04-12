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

		var connectionString = configuration.GetConnectionString("DefaultConnection");

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

		services.AddHsts(options =>
		{
			options.Preload = true;
			options.IncludeSubDomains = true;
		});

		services.AddDistributedMemoryCache();
		services.AddSession();


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
}
//	private static void ConfigureServices(IServiceCollection services)
//	{
//		var configuration = new ConfigurationBuilder()
//			.SetBasePath(Directory.GetCurrentDirectory())
//			.AddJsonFile("appsettings.json")
//			.Build();

//		var connectionString = configuration.GetConnectionString("DefaultConnection");

//		services.AddDbContext<BrickDbContext>(options =>
//			options.UseSqlServer(connectionString));

//		services.AddDefaultIdentity<IdentityUser>(options =>
//			options.SignIn.RequireConfirmedAccount = true)
//			.AddEntityFrameworkStores<BrickDbContext>();

//		services.AddControllersWithViews();

//		services.AddScoped<IBrickRepository, EFBrickRepository>();
//		services.AddTransient<ColorViewComponent>();

//		services.Configure<IdentityOptions>(options =>
//		{
//			options.Password.RequireDigit = true;
//			options.Password.RequireLowercase = true;
//			options.Password.RequireNonAlphanumeric = true;
//			options.Password.RequireUppercase = true;
//			options.Password.RequiredLength = 12;
//			options.Password.RequiredUniqueChars = 1;
//		});

//		services.AddHsts(options =>
//		{
//			options.Preload = true;
//			options.IncludeSubDomains = true;
//		});

//		services.AddDistributedMemoryCache();
//		services.AddSession();
//	}
//}



