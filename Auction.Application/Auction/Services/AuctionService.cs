using Auction.Application.Auction.Intefaces;
using Auction.Application.Auction.Requests;
using Auction.Application.Auction.Responses;
using Auction.Application.AuctionItems.Dtos;
using Auction.Application.Exceptions;
using Auction.Application.Interfaces.Infrastructure;
using Auction.Domain.Entities;

namespace Auction.Application.AuctionItems.Services;

public class AuctionService : IAuctionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuctionReadRepository _auctionReadRepository;

    public AuctionService(IUnitOfWork unitOfWork, IAuctionReadRepository auctionReadRepository)
    {
        _unitOfWork = unitOfWork;
        _auctionReadRepository = auctionReadRepository;
    }

    public async Task AddAuctionAsync(AddAuctionRequest request, CancellationToken ct = default)
    {
        var auctionItem = AuctionItem.Create(
                                Guid.CreateVersion7(),
                                request.Name,
                                request.Description,
                                request.StartingPrice,
                                request.StartDate,
                                request.EndDate);

        await _unitOfWork.Auctions.AddAsync(auctionItem, ct);

        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task BidOnAuctionAsync(BidOnAuctionRequest request, CancellationToken ct = default)
    {
        var auction = await _unitOfWork.Auctions.GetByIdAsync(request.AuctionId, ct);
}
