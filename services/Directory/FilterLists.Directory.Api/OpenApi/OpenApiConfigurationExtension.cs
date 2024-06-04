using Microsoft.OpenApi.Models;
using ConfigurationExtensions = FilterLists.Directory.Application.ConfigurationExtensions;

namespace FilterLists.Directory.Api.OpenApi;

internal static class OpenApiConfigurationExtensions
{
    internal static void AddOpenApiGen(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "FilterLists Directory API",
                    Description = "An ASP.NET Core API serving the core FilterList information.",
                    Version = "v1",
                    TermsOfService =
                        new Uri("https://github.com/collinbarrett/FilterLists/blob/main/.github/CODE_OF_CONDUCT.md"),
                    Contact = new OpenApiContact { Name = "FilterLists", Url = new Uri("https://filterlists.com") },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/collinbarrett/FilterLists/blob/main/LICENSE")
                    }
                });

            // include view model xml comments
            var xmlFilename = $"{typeof(ConfigurationExtensions).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            // include OpenApiTag Description and ExternalDocs
            options.DocumentFilter<OpenApiTags.TagDescriptionsDocumentFilter>();
        });
    }
}