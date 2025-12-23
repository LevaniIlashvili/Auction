namespace Auction.Application.Auction.Requests;

public sealed record AddAuctionRequest(
    string Name,
    string Description,
    decimal StartingPrice,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);
