using Microsoft.AspNetCore.SignalR;

namespace Auction.Api.Hubs;

public class AuctionHub : Hub
{
    public Task JoinAuction(Guid auctionId)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, auctionId.ToString());
    }

    public Task LeaveAuction(Guid auctionId)
    {
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, auctionId.ToString());
    }
}
