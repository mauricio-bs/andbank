using ApiConsole.Data;
using ApiConsole.Model;

namespace ApiConsole.Repository.Implementation;

public class PositionRepository(AppDbContext dbContext) : IPositionRepository
{
    private readonly AppDbContext _context = dbContext;

    public async Task BulkSave(List<Position> data)
    {
        await _context.Positions.AddRangeAsync(data);
        await _context.SaveChangesAsync();
    }
}
