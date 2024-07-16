using ApiRest.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Data;

public class AppDbContext : DbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    public DbSet<Position> Positions { get; set; }
}

// "Server=127.0.0.1;Port=5432;Database=StockOptions;User Id=postgres;Password=postgres;Pooling=true;MinPoolSize=1;MaxPoolSize=20;ConnectionLifeTime=15;"
