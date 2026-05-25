using Microsoft.EntityFrameworkCore;
using SmashHub.Domain;

namespace SmashHub.DataAccess
{
    public class SmashHubContext : DbContext
    {
        public SmashHubContext(DbContextOptions<SmashHubContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }
        public DbSet<StringingOrder> StringingOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<StringingOrder>()
                .Property(o => o.TotalLei)
                .HasPrecision(18, 2);

            // N:1 — StringingOrder -> User
            modelBuilder.Entity<StringingOrder>()
                .HasOne(o => o.Client)
                .WithMany(u => u.StringingOrders)
                .HasForeignKey(o => o.ClientUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserContact>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contacts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
