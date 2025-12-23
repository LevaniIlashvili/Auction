using Auction.Application.Auction.Dtos;
using Auction.Application.Auction.Intefaces;
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
}
