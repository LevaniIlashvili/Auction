namespace Auction.Application.Auction.Requests;

public sealed record BidOnAuctionRequest(Guid UserId, Guid AuctionId, decimal Amount);
