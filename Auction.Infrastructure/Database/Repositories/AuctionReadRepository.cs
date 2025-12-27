using Auction.Application.Auction.Dtos;
using Auction.Application.Auction.Intefaces;
using Auction.Application.Auction.Responses;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Database.Repositories;

public class AuctionReadRepository : IAuctionReadRepository
{
    private readonly AuctionDbContext _dbContext;

    public AuctionReadRepository(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AuctionSummaryDto>> GetAllSummariesAsync(CancellationToken ct = default)
    {
        return await _dbContext.AuctionItems
            .AsNoTracking()
            .Select(a => new AuctionSummaryDto(
                a.Id,
                a.Name,
                a.CurrentHighestBid,
                a.Status,
                a.StartDate,
                a.EndDate,
                a.Bids.Count))
            .ToListAsync(ct);
    }

    public async Task<GetAuctionDetailsResponse?> GetDetailsAsync(Guid auctionId, CancellationToken ct = default)
    {
        return await _dbContext.AuctionItems
            .AsNoTracking()
            .Where(a => a.Id == auctionId)
            .Select(a => new GetAuctionDetailsResponse(
                a.Id,
                a.Name,
                a.Description,
                a.StartingPrice,
                a.CurrentHighestBid,
                a.Status,
                a.StartDate,
                a.EndDate,
                a.WinnerId,
                a.Bids.OrderByDescending(x => x.BidTime).Select(b => new BidDto(
                    b.Id,
                    b.AuctionId,
                    b.UserId,
                    b.Amount,
                    b.BidTime)).ToList()))
            .FirstOrDefaultAsync(ct);
    }
}
