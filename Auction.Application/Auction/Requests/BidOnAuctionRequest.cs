namespace Auction.Application.Auction.Requests;

public sealed record BidOnAuctionRequest(Guid AuctionId, decimal Amount);
