using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProductManagementAPI
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                        desc.GroupName, new OpenApiInfo()
                        {
                            Title = $"Parky API {desc.ApiVersion}",
                            Version = desc.ApiVersion.ToString()
                        }
                    );
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Tiêu đề ủy quyền jwt sử dụng lược đồ Bearer. \r\n\r\n" +
                "Nhập 'Bearer' [space] rồi nhập mã thông báo của bạn vào phần nhập văn bản bên dưới.\r\n\r\n" +
                "Ví dụ: 'Bearer' 'mã token'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            var xmlComentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlComentFile);
            options.IncludeXmlComments(cmlCommentsFullPath);

        }
    }
}
