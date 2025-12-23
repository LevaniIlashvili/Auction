using Auction.Application.Auction.Intefaces;
using Auction.Application.Interfaces;
using Auction.Application.Interfaces.Infrastructure;
using Auction.Infrastructure.Database;
using Auction.Infrastructure.Database.Repositories;
using Auction.Infrastructure.Options;
using Auction.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuctionDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection(nameof(JwtSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AuctionDbContext>();

        services.AddScoped<IUserIdentityService, IdentityService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<IAuctionReadRepository, AuctionReadRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}