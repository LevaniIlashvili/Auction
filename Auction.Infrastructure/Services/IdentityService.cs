using Auction.Application.Interfaces;
using Auction.Application.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Auction.Infrastructure.Services;

public class IdentityService : IUserIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public IdentityService(UserManager<IdentityUser> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(
       string email,
       string password)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return (false, result.Errors.Select(e => e.Description));

        var roleResult = await _userManager.AddToRoleAsync(user, "User");

        if (!roleResult.Succeeded)
            return (false, roleResult.Errors.Select(e => e.Description));

        return (true, Array.Empty<string>());
    }


    public async Task<(bool Success, string Token)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return (false, string.Empty);

        if (!await _userManager.CheckPasswordAsync(user, password))
            return (false, string.Empty);

        var roles = await _userManager.GetRolesAsync(user);
        var token = await _jwtTokenService.CreateTokenAsync(user.Id, email, roles);

        return (true, token);
    }
}
