using Microsoft.EntityFrameworkCore;
using Serdiuk.Rabbit.Core.Interfaces;
using Serdiuk.Rabbit.Core.Models;

namespace Serdiuk.Rabbit.API.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Forum>().Property(p => p.LikedUsers)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
        }
    }
}
