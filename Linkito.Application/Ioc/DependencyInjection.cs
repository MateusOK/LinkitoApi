using Linkito.Application.Services;
using Linkito.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Linkito.Application.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IShortUrlService, ShortUrlService>();
        return services;
    }
}