namespace Auction.Application.Interfaces;

public interface IUserIdentityService
{
    Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(string email, string password);
    Task<(bool Success, string Token)> LoginAsync(string email, string password);
}
