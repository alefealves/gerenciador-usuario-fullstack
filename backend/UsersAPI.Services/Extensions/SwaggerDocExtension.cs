using Microsoft.OpenApi.Models;
using System.Reflection;

namespace UsersAPI.Services.Extensions
{
    public static class SwaggerDocExtension
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "UsersAPI",
                    Description = "API para gerenciar usuários",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Álefe Alves",
                        Email = "alefealves@hotmail.com"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);

                // options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                // {
                //     Name = "Authorization",
                //     Type = SecuritySchemeType.ApiKey,
                //     Scheme = "Bearer",
                //     BearerFormat = "JWT",
                //     In = ParameterLocation.Header,
                //     Description = "JWT Authorization header using the Bearer scheme."
                // });

                // options.AddSecurityRequirement(new OpenApiSecurityRequirement
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type = ReferenceType.SecurityScheme,
                //                 Id = "Bearer"
                //             },
                //             Scheme = "oauth2",
                //             Name = "Bearer",
                //             In = ParameterLocation.Header,

                //         },
                //         new List<string>()
                //     }
                // });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "UsersAPI");
            });

            return app;
        }
    }
}