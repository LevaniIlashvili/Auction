using Auction.Application.Auction.Intefaces;
using Auction.Application.Auction.Requests;
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

    [HttpPost("bid")]
    public async Task<IActionResult> PlaceBid([FromBody] BidOnAuctionRequest request, CancellationToken ct = default)
    {
        await _auctionService.BidOnAuctionAsync(request, ct);

        return Ok();
    }
}
