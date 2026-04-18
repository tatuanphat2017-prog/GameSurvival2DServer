using GameSurvival2DServer.MODELL;
using Microsoft.EntityFrameworkCore;

namespace GameSurvival2DServer.Database
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<PlayerProfile> PlayerProfiles { get; set; }
        public DbSet<SaveGame> SaveGames { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }
        public DbSet<AdminLog> AdminLogs { get; set; }
    }
}