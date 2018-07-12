
using BusinessLayer.Services;
using ClientAPI.BusinessLayer.Contracts;
using ClientAPI.BusinessLayer.Services;
using ClientAPI.Data.Contracts;
using ClientAPI.Data.Repositories;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceCollectionLayer
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services) {
            
            services.AddTransient<IRegisterService, RegisterService>();

            services.AddTransient<IAuthService, AuthService>();
            
            return services;
        }

        public static IServiceCollection AddDataLayerServices(this IServiceCollection services) {

            services.AddScoped(typeof(IUserRepo), typeof(UserRepo));

            return services;
        }
    }
}