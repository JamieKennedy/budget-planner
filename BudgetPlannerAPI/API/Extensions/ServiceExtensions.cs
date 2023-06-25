﻿using Common.Constants;

using LoggerService;
using LoggerService.Interfaces;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Repository;
using Repository.Contracts;

using Services;
using Services.Contracts;


namespace API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringBase = configuration.GetConnectionString("Default");
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionStringBase)
        {
            UserID = configuration["SqlServer:username"],
            Password = configuration["SqlServer:password"],
        };

        services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlServer(connectionStringBuilder.ConnectionString, b => b.MigrationsAssembly("API"));
        });
    }

    public static void ConfigureLoggerManager(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var corsPolicy = configuration.GetSection(ConfigurationConst.Cors.POLICY_SECTION);
        if (corsPolicy is not null)
        {
            var policyName = corsPolicy[ConfigurationConst.Cors.POLICY_NAME];

            if (policyName is not null)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(policyName, policyBuilder =>
                    {
                        // origins
                        var originsString = corsPolicy[ConfigurationConst.Cors.ALLOWED_ORIGINS];
                        if (!string.IsNullOrEmpty(originsString) && originsString.Equals(ConfigurationConst.Cors.ALLOW_ALL))
                        {
                            policyBuilder.AllowAnyOrigin();
                        }
                        else
                        {
                            var origins = corsPolicy.GetSection(ConfigurationConst.Cors.ALLOWED_ORIGINS).Get<string[]>();
                            if (origins is not null)
                            {
                                policyBuilder.WithOrigins(origins);
                            }
                        }


                        // methods
                        var methodsString = corsPolicy[ConfigurationConst.Cors.ALLOWED_METHODS];
                        if (!string.IsNullOrEmpty(methodsString) && methodsString.Equals(ConfigurationConst.Cors.ALLOW_ALL))
                        {
                            policyBuilder.AllowAnyMethod();
                        }
                        else
                        {
                            var methods = corsPolicy.GetSection(ConfigurationConst.Cors.ALLOWED_METHODS).Get<string[]>();
                            if (methods is not null)
                            {
                                policyBuilder.WithMethods(methods);
                            }
                        }

                        // Headers
                        var headersString = corsPolicy[ConfigurationConst.Cors.ALLOWED_HEADERS];
                        if (!string.IsNullOrEmpty(headersString) && headersString.Equals(ConfigurationConst.Cors.ALLOW_ALL))
                        {
                            policyBuilder.AllowAnyHeader();
                        }
                        else
                        {
                            var headers = corsPolicy.GetSection(ConfigurationConst.Cors.ALLOWED_HEADERS).Get<string[]>();
                            if (headers is not null)
                            {
                                policyBuilder.WithHeaders(headers);
                            }
                        }

                        // Credentials
                        var credentials = corsPolicy.GetSection(ConfigurationConst.Cors.ALLOW_CREDENTIALS).Get<bool>();
                        if (credentials)
                        {
                            policyBuilder.AllowCredentials();
                        }
                    });
                });
            }


        }
    }
}