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
}
