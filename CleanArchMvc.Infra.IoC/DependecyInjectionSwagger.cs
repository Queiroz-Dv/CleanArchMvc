﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependecyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchMvc.API", Version = "v1" });
                  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                  {
                      Name = "Authorization",
                      Type = SecuritySchemeType.ApiKey,
                      Scheme = "Bearer",
                      BearerFormat = "JWT",
                      In = ParameterLocation.Header
                  });

                  c.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type= ReferenceType.SecurityScheme,
                                Id= "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                  });

              });

            return services;
        }
    }
}
