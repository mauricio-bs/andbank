using ApiRest.Domain.DTO;
using ApiRest.Model;

namespace ApiRest.Domain.Service;

public interface IPositionService
{
    public Task<List<Position?>> ListClientPositions(string ClientId);
    public Task<List<PositionSummary>> ListClientPositionsSummary(string ClientId);

    public Task<List<Position>> ListTopPositions();
}
