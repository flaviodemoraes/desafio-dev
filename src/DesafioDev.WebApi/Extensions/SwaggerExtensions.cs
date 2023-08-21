using Microsoft.OpenApi.Models;

namespace DesafioDev.WebApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static WebApplicationBuilder SetSwaggerGenDefinitions(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(o =>
            {
                o.EnableAnnotations();

                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sistema de Registros CNAB.",
                    Version = "v1",
                });
            });

            return builder;
        }
    }
}
