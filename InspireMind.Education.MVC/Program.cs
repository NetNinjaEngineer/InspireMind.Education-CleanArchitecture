using InspireMind.Education.MVC.Contracts;
using InspireMind.Education.MVC.Helpers;
using InspireMind.Education.MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient(Constants.AuthClient, options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("ApiBaseUrl").Value!);
    options.DefaultRequestHeaders.Clear();
    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient(Constants.CoursesClient, options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("ApiBaseUrl").Value!);
    options.DefaultRequestHeaders.Clear();
    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient(Constants.TopicsClient, options =>
{
    options.BaseAddress = new Uri(builder.Configuration.GetSection("ApiBaseUrl").Value!);
    options.DefaultRequestHeaders.Clear();
    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Home/AccessDenied");
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
