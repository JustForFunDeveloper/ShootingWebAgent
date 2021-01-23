using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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

        /// <summary>
        /// Creates a md5Hash from the given string.
        /// If the string is null it will generate a basic random string itself.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMd5Hash(this string value)
        {
            Random random = new Random();
            string text;
            if (value == null)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                text = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                text += DateTime.UtcNow.Millisecond;
            }
            else
            {
                text = value + DateTime.UtcNow.Millisecond;
            }
            
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(text);
            byte[] result = md5.ComputeHash(textToHash);
            Thread.Sleep(1);

            return BitConverter.ToString(result); 
        }
    }
}