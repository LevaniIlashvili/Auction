using Auction.Api.Hubs;
using Auction.Application.Auction.Dtos;
using Auction.Application.Auction.Intefaces;
using Auction.Application.Auction.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Auction.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuctionController : ControllerBase
{
    private readonly IAuctionService _auctionService;
    private readonly IHubContext<AuctionHub> _hubContext;

    public AuctionController(IAuctionService auctionService, IHubContext<AuctionHub> hubContext)
    {
        _auctionService = auctionService;
        _hubContext = hubContext;
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

        var bidRequest = request with { AuctionId = auctionId };

        var userId = Guid.Parse(userIdClaim);
        var bid = await _auctionService.BidOnAuctionAsync(userId, bidRequest, ct);

        await _hubContext.Clients
            .Group(auctionId.ToString())
            .SendAsync("BidPlaced", new BidDto(bid.Id, bid.AuctionId, bid.UserId, bid.Amount, bid.BidTime), ct);

        return Ok();
    }
}
