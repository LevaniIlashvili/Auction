using Auction.Application.Auction.Intefaces;
using Auction.Application.Auction.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuctionService, AuctionService>();

        return services;
    }
}
