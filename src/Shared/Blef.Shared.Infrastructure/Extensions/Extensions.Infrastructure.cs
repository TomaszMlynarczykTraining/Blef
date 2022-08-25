﻿using System.Runtime.CompilerServices;
using Blef.Shared.Abstractions.Modules;
using Blef.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

[assembly: InternalsVisibleTo("Blef.Bootstrapper")]

namespace Blef.Shared.Infrastructure.Extensions;

internal static partial class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IEnumerable<IModule> modules) =>
        services
            .AddControllers(configuration)
            .AddModuleInfo(modules)
            .AddSwagger();

    public static IApplicationBuilder UseInfrastructure(this WebApplication application) =>
        application
            .UseDevelopment()
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMainInfo();
                endpoints.MapModuleInfo();
            })
            .UseSwagger()
            .UseSwaggerUI(c =>
                c.SwaggerEndpoint(
                    name: "Blef",
                    url: "/swagger/v1/swagger.json"));

    private static IServiceCollection AddControllers(this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddControllers()
            .ConfigureApplicationPartManager(manager =>
                manager.AddOnlyNotDisabledModuleParts(configuration.DetectDisabledModules()))
            .Services;

    private static IServiceCollection AddSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);
            options.SwaggerDoc(name: "v1", info: new OpenApiInfo
            {
                Title = "Blef",
                Version = "v1",
                Description = "Card Game",
                Contact = new OpenApiContact
                {
                    Name = "Artur Wincenciak",
                    Email = "artur.wincenciak@gmial.com",
                    Url = new Uri(uriString: "https://teovincent.com")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri(uriString: "https://github.com/ArturWincenciak/Blef/blob/main/LICENSE")
                }
            });
        });

    private static IApplicationBuilder UseDevelopment(this WebApplication app) =>
        app.Environment.IsDevelopment() switch
        {
            true => app.UseDeveloperExceptionPage(),
            _ => app
        };
}