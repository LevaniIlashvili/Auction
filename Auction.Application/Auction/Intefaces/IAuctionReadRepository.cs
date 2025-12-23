using Auction.Application.Auction.Responses;
using Auction.Application.Auction.Dtos;

namespace Auction.Application.Auction.Intefaces;

public interface IAuctionReadRepository
{
    Task<List<AuctionSummaryDto>> GetAllSummariesAsync(CancellationToken ct = default);
    Task<GetAuctionDetailsResponse?> GetDetailsAsync(Guid auctionId, CancellationToken ct = default);
}
