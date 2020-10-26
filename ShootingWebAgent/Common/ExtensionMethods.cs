using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ShootingWebAgent.Common
{
    public static class ExtensionMethods
    {
        public static IHost UpdateDataDatabase<T>(this IHost webHost) where T : DbContext
        {
            var serviceScopeFactory = (IServiceScopeFactory) webHost.Services.GetService(typeof(IServiceScopeFactory));
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Starting update of the database!");
                var dbContext = services.GetRequiredService<T>();
                try
                {
                    dbContext.GetService<IMigrator>().Migrate();
                }
                catch (Exception e)
                {
                    logger.LogError("Failed to update the database!", e);
                }

                logger.LogInformation("Finished update of the database!");
            }

            return webHost;
        }

        public static string ToJsonString(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        
        public static T ToJsonObject<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}