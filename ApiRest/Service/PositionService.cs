using ApiRest.Domain.DTO;
using ApiRest.Domain.Service;
using ApiRest.Model;
using ApiRest.Repository;

namespace ApiRest.Service;

public class PositionService(IPositionRepository repository) : IPositionService
{
    private readonly IPositionRepository _repository = repository;

    public async Task<List<Position?>> ListClientPositions(string ClientId)
    {
        return await _repository.ListClientPositions(ClientId);
    }

    public async Task<List<PositionSummary>> ListClientPositionsSummary(string ClientId)
    {
        return await _repository.ListLastClientPositions(ClientId);
    }

    public async Task<List<Position>> ListTopPositions()
    {
        return await _repository.ListTopPositions();
    }
}
