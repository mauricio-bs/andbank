using Microsoft.EntityFrameworkCore;

namespace ApiConsole.Data;

public class Migrations(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task ExecuteMigrations()
    {
        await _context.Database.MigrateAsync();
    }
}
