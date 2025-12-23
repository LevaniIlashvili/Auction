using Auction.Application.Auction.Requests;
using Auction.Application.Auction.Responses;
using Auction.Application.AuctionItems.Dtos;
using Auction.Application.Auction.Dtos;

namespace Auction.Application.Auction.Intefaces;

public interface IAuctionService
{
    Task AddAuctionAsync(AddAuctionRequest request, CancellationToken ct = default);
}
