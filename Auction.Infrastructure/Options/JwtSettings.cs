using System.ComponentModel.DataAnnotations;

namespace Auction.Infrastructure.Options;

public class JwtSettings
{
    [Required]
    public string Secret { get; init; } = default!;
    [Required]
    public string Issuer { get; init; } = default!;
    [Required]
    public string Audience { get; init; } = default!;
}
