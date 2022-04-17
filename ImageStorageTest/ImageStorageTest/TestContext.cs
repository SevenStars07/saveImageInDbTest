using Microsoft.EntityFrameworkCore;

namespace ImageStorageTest;

public class TestContext: DbContext
{
    public TestContext(DbContextOptions<TestContext> options) : base(options) { }

    public DbSet<Image> Images { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bid>()
            .HasKey(bid => new {bid.BidderId, bid.AuctionId});
        
        modelBuilder.Entity<Bid>()
            .HasOne(b=>b.Bidder)
            .WithMany(u=>u.Bids)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Auction>()
            .HasOne(a => a.Seller)
            .WithMany(u => u.Auctions)
            .OnDelete(DeleteBehavior.NoAction);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=ImageStorageTest;Trusted_Connection=True;");
        }
    }
}