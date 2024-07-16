using System.ComponentModel.DataAnnotations;

namespace ApiRest.Model;

public class Position
{
    [Key]
    public required string Id { get; init; }

    public required string PositionId { get; set; }

    public required string ProductId { get; set; }

    public required string ClientId { get; set; }

    public required DateOnly Date { get; set; }

    public required decimal Value { get; set; }

    public required decimal Quantity { get; set; }
}