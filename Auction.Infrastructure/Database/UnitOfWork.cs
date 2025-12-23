using Auction.Application.Auction.Intefaces;
using Auction.Application.Interfaces.Infrastructure;

namespace Auction.Infrastructure.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly AuctionDbContext _auctionDbContext;
    public IAuctionRepository Auctions { get; }

    public UnitOfWork(AuctionDbContext dbContext, IAuctionRepository auctionRepository)
    {
        _auctionDbContext = dbContext;
        Auctions = auctionRepository;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _auctionDbContext.SaveChangesAsync(ct);
    }
}
