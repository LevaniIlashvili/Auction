using Auction.Application.Auction.Intefaces;
using Auction.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Database.Repositories;

public class AuctionRepository : IAuctionRepository
{
    private readonly AuctionDbContext _dbContext;

    public AuctionRepository(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuctionItem?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.AuctionItems
            .Include(a => a.Bids)
            .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task AddAsync(AuctionItem auction, CancellationToken ct = default)
    {
        await _dbContext.AuctionItems.AddAsync(auction, ct);
    }

    public void Remove(AuctionItem auction)
    {
        _dbContext.AuctionItems.Remove(auction);
    }
}
