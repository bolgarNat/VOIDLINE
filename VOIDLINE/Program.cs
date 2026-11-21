using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VOIDLINE.Models.Database;
using VOIDLINE.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddSingleton<PasswordService>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var key = config["PasswordHash:Key"]
        ?? throw new InvalidOperationException("PasswordHash:Key is not configured");

    return new PasswordService(
        System.Text.Encoding.UTF8.GetBytes(key)
    );
});

builder.Services.AddSingleton<LoggerService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddControllersWithViews();

// Authentication via cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");
// Authorization
builder.Services.AddAuthorization();

builder.Services.AddScoped(typeof(DatabaseService<>));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Authentication middleware 
app.UseAuthentication();
// Authorization middleware 
app.UseAuthorization();

// Access to wwwroot files
app.UseStaticFiles();

app.MapStaticAssets();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}")
    .WithStaticAssets();

app.Run();
