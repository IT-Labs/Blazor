using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Framework
{
    public static class StartUpExtensions
    {
        public static void ConfigureDatabase<T>(this IConfigurationRoot configuration, IServiceCollection services)
            where T : DbContext
        {
            services.AddDbContext<T>(options =>
                options.UseInMemoryDatabase(databaseName: "InMemoryDb"));
        }
    }
}
