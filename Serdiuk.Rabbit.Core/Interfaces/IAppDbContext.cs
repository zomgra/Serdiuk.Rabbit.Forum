using Microsoft.EntityFrameworkCore;
using Serdiuk.Rabbit.Core.Models;

namespace Serdiuk.Rabbit.Core.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Comment> Comments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
