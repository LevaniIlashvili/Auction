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

    public async Task AddAsync(AuctionItem auction, CancellationToken ct = default)
    {
        await _dbContext.AuctionItems.AddAsync(auction, ct);
    }
}
