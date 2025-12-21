using Auction.Domain.Enums;
using Auction.Domain.Exceptions;

namespace Auction.Domain.Entities;

public class AuctionItem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal StartingPrice { get; private set; }
    public decimal CurrentHighestBid { get; private set; }
    public Guid? CurrentHighestBidderId { get; private set; }
    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset EndDate { get; private set; }
    public AuctionStatus Status { get; private set; }

    public IReadOnlyCollection<Bid> Bids => _bids;
    private readonly List<Bid> _bids = new();

    public Guid? WinnerId { get; private set; }

    private AuctionItem() { }

    private AuctionItem(
        Guid id,
        string name,
        string description,
        decimal startingPrice,
        DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        Id = id;
        Name = name;
        Description = description;
        StartingPrice = startingPrice;
        CurrentHighestBid = 0;
        StartDate = startDate;
        EndDate = endDate;
        Status = AuctionStatus.Pending;
    }

    public static AuctionItem Create(
        Guid id,
        string name,
        string description,
        decimal startingPrice,
        DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Auction name cannot be empty.");

        if (startingPrice < 0)
            throw new DomainException("Starting price cannot be negative.");

        if (endDate <= startDate)
            throw new DomainException("End date must be after the start date.");

        if (startDate < DateTimeOffset.UtcNow.AddMinutes(-1))
            throw new DomainException("Start date cannot be in the past.");

        return new AuctionItem(id, name, description, startingPrice, startDate, endDate);
    }

    public Bid PlaceBid(Guid userId, decimal amount, DateTimeOffset currentTime)
    {
        if (Status != AuctionStatus.Active)
            throw new DomainException("Bidding is only allowed on active auctions.");

        if (currentTime < StartDate)
            throw new DomainException("This auction hasn't started yet.");

        if (currentTime > EndDate)
            throw new DomainException("This auction has already ended.");

        if (userId == CurrentHighestBidderId)
            throw new DomainException("You are already the highest bidder.");

        var minimumBid = CurrentHighestBid > 0
            ? CurrentHighestBid
            : StartingPrice;

        if (amount <= minimumBid)
            throw new DomainException($"Bid must be higher than {minimumBid}.");

        var bid = Bid.Create(Id, userId, amount);

        _bids.Add(bid);
        CurrentHighestBid = amount;
        CurrentHighestBidderId = userId;

        return bid;
    }

    public void Activate()
    {
        if (Status != AuctionStatus.Pending)
            throw new DomainException("Only pending auctions can be activated.");

        Status = AuctionStatus.Active;
    }

    public void Finish()
    {
        if (Status != AuctionStatus.Active)
            throw new DomainException("Only active auctions can be finished.");

        Status = AuctionStatus.Finished;
        WinnerId = CurrentHighestBidderId;
    }
}