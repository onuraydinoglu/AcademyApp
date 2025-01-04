using System.Net;
using AcademyApp.Repository;
using AcademyApp.Repository.Abstracts;
using AcademyApp.Repository.Concretes;
using AcademyApp.Repository.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILevelRepository, LevelRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Kullanıcı oturum açmadığında yönlendirilmesi gereken giriş sayfası
        options.LoginPath = "/Users/Login"; // Login sayfasının yolu

        // Kullanıcı oturum açmış ancak yetkisi yoksa yönlendirilmesi gereken sayfa
        options.AccessDeniedPath = "/Home/Index"; // Yetkisiz erişim sayfasının yolu

        // Cookie ömrü (örneğin: 1 saat boyunca oturum geçerli olacak)
        options.ExpireTimeSpan = TimeSpan.FromHours(1);

        // Kalıcı oturum (Persistent cookie) için "IsPersistent" ayarına izin ver
        options.SlidingExpiration = true; // Oturum süresini kullanıcı etkinliğine göre uzatır

        // Cookie adı (tarayıcıda saklanacak cookie'nin adı)
        options.Cookie.Name = "AcademyApp.Auth";

        // HTTPS gereksinimi
        options.Cookie.HttpOnly = true; // Cookie sadece HTTP isteklerinde gönderilir, JavaScript'ten erişilemez
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS bağlantılarda geçerli

        // Logout sonrası yönlendirme
        options.LogoutPath = "/Users/Logout";
    });


var app = builder.Build();

SeedData.TestData(app);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
