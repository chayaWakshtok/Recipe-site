using BL;
using BL.Repository;
using BL.Services;
using DAL.Interfaces;
using DAL.Interfaces.Services;
using DAL.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace Netcore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add needed instances for database
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            // Configure DbContext with Scoped lifetime   
            services.AddDbContext<RecipesSiteContext>(options =>
            {
                options.UseSqlServer("");
            }
            );

            services.AddScoped<Func<RecipesSiteContext>>((provider) => () => provider.GetService<RecipesSiteContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        /// <summary>
        /// Add instances of in-use services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
