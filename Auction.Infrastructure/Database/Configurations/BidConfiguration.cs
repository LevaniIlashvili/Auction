using Auction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Infrastructure.Database.Configurations;

public class BidConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(b => b.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(b => b.BidTime).IsRequired();

        builder.HasOne(b => b.AuctionItem)
            .WithMany(a => a.Bids)
            .HasForeignKey(b => b.AuctionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(b => b.UserId)
            .IsRequired();
    }
}
