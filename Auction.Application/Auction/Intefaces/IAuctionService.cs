using Auction.Application.Auction.Requests;
using Auction.Application.Auction.Responses;
using Auction.Application.Auction.Dtos;

namespace Auction.Application.Auction.Intefaces;

public interface IAuctionService
{
    Task AddAuctionAsync(AddAuctionRequest request, CancellationToken ct = default);
    Task<List<AuctionSummaryDto>> GetAuctionSummariesAsync(CancellationToken ct = default);
    Task<GetAuctionDetailsResponse> GetAuctionDetailsAsync(Guid auctionId, CancellationToken ct = default);
    Task BidOnAuctionAsync(BidOnAuctionRequest request, CancellationToken ct = default);
}
