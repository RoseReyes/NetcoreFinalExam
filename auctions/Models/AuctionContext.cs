using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
 
namespace auctions.Models
{
    public class AuctionContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set;}
        public DbSet<Like> Likes { get; set; }
    }
}
