using Auction.Domain.Entities;

namespace Auction.Application.Auction.Intefaces;

public interface IAuctionRepository
{
    Task<AuctionItem?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(AuctionItem auction, CancellationToken ct = default);
    void Remove(AuctionItem auction);
}
