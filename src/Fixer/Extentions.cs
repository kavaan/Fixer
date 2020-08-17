using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fixer.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fixer
{
    public static class Extensions
    {
        public static IFixerBuilder AddFixer(this IServiceCollection services,
            string sectionName = "app")
        {
            var builder = FixerBuilder.Create(services);
            var options = builder.GetOptions<AppOptions>(sectionName);
            services.AddSingleton(options);
            services.AddSingleton<IServiceId, ServiceId>();
            if (!options.DisplayBanner || string.IsNullOrWhiteSpace(options.Name))
            {
                Console.WriteLine(Figgle.FiggleFonts.Doom.Render($"{options.Name}"));
            }

            return builder;
        }

        public static IApplicationBuilder UseConvey(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IStartupInitializer>();
                Task.Run(() => initializer.InitializeAsync()).GetAwaiter().GetResult();
            }

            return app;
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
            where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }

        public static TModel GetOptions<TModel>(this IFixerBuilder builder, string settingsSectionName)
            where TModel : new()
        {
            using (var serviceProvider = builder.Services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                return configuration.GetOptions<TModel>(settingsSectionName);
            }
        }

        public static IApplicationBuilder UseInitializers(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IStartupInitializer>();
                if (initializer is null)
                {
                    throw new InvalidOperationException("Startup initializer was not found.");
                }
                Task.Run(() => initializer.InitializeAsync());
            }

            return app;
        }

        public static string Underscore(this string value)
            => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
    }
}
