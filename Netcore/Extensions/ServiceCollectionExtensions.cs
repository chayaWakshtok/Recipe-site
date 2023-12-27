using BL;
using BL.Interfaces;
using BL.Interfaces.Services;
using BL.Services;
using DAL.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
                options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=RecipesSite;Integrated Security=true");
            }
            );

            services.AddScoped<Func<RecipesSiteContext>>((provider) => () => provider.GetService<RecipesSiteContext>());

            return services;
        }

        /// <summary>
        /// Add instances of in-use services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IAuthRepository, AuthRepository>()
                 .AddScoped<IDifficultyService, DifficultyService>()
                .AddScoped<IUserService, UserService>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
