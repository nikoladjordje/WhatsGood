using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Xml.Linq;
using WhatsGoodApi.Models;
using Microsoft.EntityFrameworkCore;
namespace WhatsGoodApi.WGDbContext
{
    public class WhatsGoodDbContext : DbContext
    {
        public WhatsGoodDbContext(DbContextOptions<WhatsGoodDbContext> options) : base(options) { }
        public DbSet<User>? Users { get; set; }
        public DbSet<Message>? Messages { get; set; }
        public DbSet<Friendship>? Friendships { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friendship>()
                .HasOne(fl => fl.User)
                .WithMany(u => u.InitiatorFriendships)
                .HasForeignKey(fl => fl.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friendship>()
                .HasOne(fl => fl.Friend)
                .WithMany(u => u.FriendFriendships)
                .HasForeignKey(fl => fl.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(fl => fl.Sender)
                .WithMany(u => u.SenderLists)
                .HasForeignKey(fl => fl.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(fl => fl.Recipient)
                .WithMany(u => u.RecipientLists)
                .HasForeignKey(fl => fl.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
