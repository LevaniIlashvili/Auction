using Auction.Application.Auction.Requests;
using Auction.Application.Auction.Responses;
using Auction.Application.Auction.Dtos;
using Auction.Domain.Entities;

namespace Auction.Application.Auction.Intefaces;

public interface IAuctionService
{
    Task AddAuctionAsync(AddAuctionRequest request, CancellationToken ct = default);
    Task<List<AuctionSummaryDto>> GetAuctionSummariesAsync(CancellationToken ct = default);
    Task<GetAuctionDetailsResponse> GetAuctionDetailsAsync(Guid auctionId, CancellationToken ct = default);
    Task<Bid> BidOnAuctionAsync(Guid userId, BidOnAuctionRequest request, CancellationToken ct = default);
}
