using ApiRest.Data;
using ApiRest.Domain.DTO;
using ApiRest.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Repository.Implementation;

public class PositionRepository(AppDbContext dbContext) : IPositionRepository
{
    private readonly AppDbContext _context = dbContext;

    public async Task<List<Position?>> ListClientPositions(string ClientId)
    {
        return await _context
            .Positions.Where(p => p.ClientId == ClientId)
            .GroupBy(p => p.PositionId)
            .Select(p => p.OrderByDescending(p => p.Date).FirstOrDefault())
            .Take(10)
            .ToListAsync();
    }

    public async Task<List<PositionSummary>> ListLastClientPositions(string ClientId)
    {
        return await _context
            .Positions.Where(p => p.ClientId == ClientId)
            .GroupBy(p => new { p.PositionId, p.ProductId })
            .Select(position => new PositionSummary
            {
                Id = position.OrderByDescending(p => p.Date).First().Id,
                PositionId = position.Key.PositionId,
                ProductId = position.Key.ProductId,
                ClientId = ClientId,
                Date = position.Max(p => p.Date),
                TotalValue = position.Sum(p => p.Value),
                TotalQuantity = position.Sum(p => p.Quantity)
            })
            .Take(10)
            .ToListAsync();
    }

    public async Task<List<Position>> ListTopPositions()
    {
        var top10Ids = await _context
            .Positions.OrderByDescending(p => p.Value)
            .Select(p => p.Id)
            .Take(10)
            .ToListAsync();

        return await _context
            .Positions.Where(p => top10Ids.Contains(p.Id))
            .OrderByDescending(p => p.Value)
            .ToListAsync();
    }
}
