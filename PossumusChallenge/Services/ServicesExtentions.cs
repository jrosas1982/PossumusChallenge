using API.PossumusChallenge.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.PossumusChallenge.Services
{
    public static class ServicesExtensions
    {
        //No usado en esta implementación
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ApiKeyAuth>();
            return services;
        }
    }
}