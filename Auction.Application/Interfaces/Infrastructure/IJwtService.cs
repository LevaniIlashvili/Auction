namespace Auction.Application.Interfaces.Infrastructure;

public interface IJwtTokenService
{
    Task<string> CreateTokenAsync(
        string userId,
        string email,
        IList<string> roles);
}
