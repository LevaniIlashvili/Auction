using Auction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.Infrastructure.Database.Configurations;

public class AuctionItemConfiguration : IEntityTypeConfiguration<AuctionItem>
{
    public void Configure(EntityTypeBuilder<AuctionItem> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Description).IsRequired().HasMaxLength(500);

        builder.Property(a => a.StartDate).IsRequired();

        builder.Property(a => a.EndDate).IsRequired();

        builder.Property(a => a.StartingPrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(a => a.CurrentHighestBid).HasPrecision(18, 2);

        builder.Property(a => a.Status).IsRequired();

        builder.Property<uint>("Version")
            .IsRowVersion();
    }
}
