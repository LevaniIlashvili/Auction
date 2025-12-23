using Auction.Application.Auction.Intefaces;

namespace Auction.Application.Interfaces.Infrastructure;

public interface IUnitOfWork
{
    IAuctionRepository Auctions { get; }
    Task SaveChangesAsync(CancellationToken ct = default);
}
