using MayetteRice.DataAccess.Data;
using MayetteRice.DataAccess.Repository;
using MayetteRice.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MayetteRice.Utility;
using Stripe;
using MayetteRice.DataAccess.DbInitializer;

var builder = WebApplication.CreateBuilder(args);

/* Add services to the container. */
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Addition of configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// Addition of configuration for Facebook
builder.Services.AddAuthentication().AddFacebook(option =>
{
    option.AppId = "434584972375689";
    option.AppSecret = "6971a6677e4a205bbb101ea5c7debc9e";
});

// Addition of Session to the services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Addition of DbInitializer
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

// This allows the application to use Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


var app = builder.Build();

/* REQUEST PIPELINE */
/* Configure the HTTP request pipeline. */
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// This is added so that the app is configured to use session.
app.UseSession();

// Invocation of SeedDatabase; This will invoke once aan application is restarted
SeedDatabase();

// This will make sure that it adds a routing that is needed to map the Razor Pages
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();


void SeedDatabase() 
{
    using (var scope = app.Services.CreateScope()) 
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
