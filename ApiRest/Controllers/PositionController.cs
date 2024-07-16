using ApiRest.Domain.DTO;
using ApiRest.Domain.Service;
using ApiRest.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers;

[Controller]
[Route("api/positions")]
public class PositionController(IPositionService service) : ControllerBase
{
    private readonly IPositionService _service = service;

    [HttpGet("client/{ClientId}")]
    [ProducesResponseType(typeof(List<Position>), StatusCodes.Status200OK)]
    public async Task<IResult> ListClientPositions([FromRoute] string ClientId)
    {
        var positions = await _service.ListClientPositions(ClientId);

        return Results.Ok(positions);
    }

    [HttpGet("client/{ClientId}/summary")]
    [ProducesResponseType(typeof(List<PositionSummary>), StatusCodes.Status200OK)]
    public async Task<IResult> ClientPositionSummary([FromRoute] string ClientId)
    {
        var positions = await _service.ListClientPositionsSummary(ClientId);

        return Results.Ok(positions);
    }

    [HttpGet("top10")]
    [ProducesResponseType(typeof(List<Position>), StatusCodes.Status200OK)]
    public async Task<IResult> TopPositions()
    {
        var positions = await _service.ListTopPositions();

        return Results.Ok(positions);
    }
}
