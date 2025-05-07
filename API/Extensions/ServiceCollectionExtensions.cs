using Ecommerce.BLL;
using Ecommerce.DAL;
using Ecommerce.Shared.Abstractions;


namespace Ecommerce.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICartsService,CartsService>();
            services.AddScoped<ICartsRepository,CartsRepository>();

            return services;
        }
    }
}
