using Microsoft.EntityFrameworkCore;
using T1_PR2_API.Model;

namespace T1_PR2_API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavGame> FavGames { get; set; }
    }
}
