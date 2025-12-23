using Auction.Domain.Entities;

namespace Auction.Application.Auction.Intefaces;

public interface IAuctionRepository
{
    Task AddAsync(AuctionItem auction, CancellationToken ct = default);
}
