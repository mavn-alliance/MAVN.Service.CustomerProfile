using System;
using AutoMapper;
using JetBrains.Annotations;
using Lykke.Sdk;
using MAVN.Service.CustomerProfile.MappingProfiles;
using MAVN.Service.CustomerProfile.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MAVN.Service.CustomerProfile.Auth;
using MAVN.Service.CustomerProfile.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.CustomerProfile
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "CustomerProfile API",
            ApiVersion = "v1"
        };

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "CustomerProfileLog";
                    logs.AzureTableConnectionStringResolver = settings => settings.CustomerProfileService.Db.LogsConnString;

                    options.Extend = (sc, settings) =>
                    {
                        sc
                            .Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
                            {
                                apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
                            })
                            .AddAuthentication(KeyAuthOptions.AuthenticationScheme)
                            .AddScheme<KeyAuthOptions, KeyAuthHandler>(KeyAuthOptions.AuthenticationScheme, "", opts => { });

                        sc.AddAutoMapper(typeof(AutoMapperProfile));
                    };
                };

                options.Swagger = swagger =>
                {
                    swagger.OperationFilter<ApiKeyHeaderOperationFilter>();
                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IMapper mapper)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.WithMiddleware = x =>
                {
                    x.UseEncryptionKey();
                    x.UseMiddleware<BadRequestExceptionMiddleware>();
                    x.UseAuthentication();
                };
            });
        }
    }
}
