using ApiRest.Domain.DTO;
using ApiRest.Model;

namespace ApiRest.Repository;

public interface IPositionRepository
{
    public Task<List<Position?>> ListClientPositions(string ClientId);
    public Task<List<PositionSummary>> ListLastClientPositions(string ClientId);

    public Task<List<Position>> ListTopPositions();
}
