using InspireMind.Education.Api;
using InspireMind.Education.Api.Extensions.Localization;
using InspireMind.Education.Api.Extensions.Swagger;
using InspireMind.Education.Api.Middleware;
using InspireMind.Education.Application;
using InspireMind.Education.Identity;
using InspireMind.Education.Identity.Entities;
using InspireMind.Education.Infrastructure;
using InspireMind.Education.Persistence;
using InspireMind.Education.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
builder.Services.AddSwaggerDocumentation();
builder.Services.AddLocalization();
builder.Services
    .AddApplicationDependencies(builder.Configuration)
    .AddInfrastructureDependencies(builder.Configuration)
    .AddPersistenceDependencies(builder.Configuration)
    .AddIdentityDependencies(builder.Configuration)
    .AddServiceDependencies()
    .AddApiDependencies(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<MigrateDatabaseMiddleware>();

if (app.Environment.IsDevelopment())
    app.UseSwaggerDocumentation();

app.UseStaticFiles();

app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.UseLocalization();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
