using Auction.Domain.Enums;

namespace Auction.Application.Auction.Dtos;

public sealed record AuctionSummaryDto(
    Guid Id,
    string Name,
    decimal CurrentHighestBid,
    AuctionStatus Status,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    int BidCount);
