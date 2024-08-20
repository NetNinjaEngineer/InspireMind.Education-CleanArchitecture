using InspireMind.Education.Application;
using InspireMind.Education.Application.Middleware;
using InspireMind.Education.Identity;
using InspireMind.Education.Identity.Entities;
using InspireMind.Education.Infrastructure;
using InspireMind.Education.Persistence;
using InspireMind.Education.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.InternalServerError));
    options.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.BadRequest));
    options.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.UnprocessableEntity));
    options.OutputFormatters.RemoveType<StringOutputFormatter>();
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalization();
builder.Services
    .AddApplicationDependencies(builder.Configuration)
    .AddInfrastructureDependencies(builder.Configuration)
    .AddPersistenceDependencies(builder.Configuration)
    .AddIdentityDependencies(builder.Configuration)
    .AddServiceDependencies();

builder.Services.AddCors(options =>
{
    var clientUrl = builder.Configuration.GetSection("ClientUrl").Value!;

    options.AddPolicy("CorsPolicy", options =>
        options.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins(clientUrl));
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var database = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await database.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

var supportedCultures = new[] { "en-US", "ar-EG", "en" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
