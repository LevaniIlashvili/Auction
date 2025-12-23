namespace Auction.Application.Auction.Dtos;

public sealed record BidDto(
    Guid Id,
    Guid AuctionId,
    Guid UserId,
    decimal Amount,
    DateTimeOffset BidTime);