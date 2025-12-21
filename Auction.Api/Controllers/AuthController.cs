using Auction.Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserIdentityService _identity;

    public AuthController(IUserIdentityService identity)
    {
        _identity = identity;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _identity.RegisterAsync(
            request.Email, request.Password);

        return result.Success ? Ok() : BadRequest(string.Join("", result.Errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _identity.LoginAsync(
            request.Email, request.Password);

        return result.Success
            ? Ok(new { token = result.Token })
            : Unauthorized("Invalid Credentials");
    }
}
