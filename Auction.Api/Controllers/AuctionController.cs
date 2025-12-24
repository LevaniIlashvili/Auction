using Auction.Application.Auction.Intefaces;
using Auction.Application.Auction.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuctionController : ControllerBase
{
    private readonly IAuctionService _auctionService;

    public AuctionController(IAuctionService auctionService)
    {
        _auctionService = auctionService;        
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAuction([FromBody] AddAuctionRequest request, CancellationToken ct = default)
    {
        await _auctionService.AddAuctionAsync(request, ct);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAuctionSummaries(CancellationToken ct = default)
    {
        var auctions = await _auctionService.GetAuctionSummariesAsync(ct);

        return Ok(auctions);
    }

    [HttpGet("{auctionId}")]
    public async Task<IActionResult> GetAuctionDetails([FromRoute] Guid auctionId, CancellationToken ct = default)
    {
        var auctionDetails = await _auctionService.GetAuctionDetailsAsync(auctionId, ct);

        return Ok(auctionDetails);
    }

    [HttpPost("{auctionId}/bids")]
    [Authorize]
    public async Task<IActionResult> PlaceBid([FromRoute] Guid auctionId, [FromBody] BidOnAuctionRequest request, CancellationToken ct = default)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return Unauthorized();

        var bid = request with { AuctionId = auctionId };

        await _auctionService.BidOnAuctionAsync(Guid.Parse(userIdClaim), bid, ct);

        return Ok();
    }
}
