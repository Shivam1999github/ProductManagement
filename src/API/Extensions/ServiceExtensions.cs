using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity;
using Infrastructure.Repositories;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection
            AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped<IProductService,
                ProductService>();

            //services.AddScoped<IItemService,
            //    ItemService>();

            services.AddScoped<IUserService,
                UserService>();

            services.AddScoped<IAuthService,
                AuthService>();

            services.AddScoped<IProductRepository,
                ProductRepository>();

            //services.AddScoped<IItemRepository,
            //    ItemRepository>();

            services.AddScoped<IUserRepository,
                UserRepository>();

            services.AddScoped<
                IRefreshTokenRepository,
                RefreshTokenRepository>();

            services.AddScoped<IUnitOfWork,
                UnitOfWork>();

            services.AddScoped<IJwtTokenGenerator,
                JwtTokenGenerator>();

            return services;
        }
    }
}
