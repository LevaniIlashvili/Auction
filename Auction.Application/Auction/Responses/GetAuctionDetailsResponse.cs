using Auction.Application.Auction.Dtos;
using Auction.Domain.Enums;

namespace Auction.Application.Auction.Responses;

public sealed record GetAuctionDetailsResponse(
    Guid Id,
    string Name,
    string Description,
    decimal StartingPrice,
    decimal CurrentHighestBid,
    AuctionStatus Status,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    Guid? WinnerId,
    List<BidDto> Bids);
