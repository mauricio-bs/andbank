using ApiConsole.Model;

namespace ApiConsole.Repository;

public interface IPositionRepository
{
    public Task BulkSave(List<Position> data);
}
