using Auction.Domain.Exceptions;

namespace Auction.Domain.Entities;

public class Bid
{
    public Guid Id { get; private set; }
    public Guid AuctionId { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTimeOffset BidTime { get; private set; }

    public AuctionItem AuctionItem { get; private set; } = null!;

    private Bid() { }

    private Bid(Guid id, Guid auctionId, Guid userId, decimal amount, DateTimeOffset bidTime)
    {
        Id = id;
        AuctionId = auctionId;
        UserId = userId;
        Amount = amount;
        BidTime = bidTime;
    }

    public static Bid Create(Guid auctionId, Guid userId, decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Bid amount must be positive.");

        return new Bid(
            Guid.NewGuid(),
            auctionId,
            userId,
            amount,
            DateTimeOffset.UtcNow);
    }
}