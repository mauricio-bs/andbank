namespace ApiRest.Domain.DTO;

public class PositionSummary
{
    public required string Id { get; set; }
    public required string PositionId { get; set; }
    public required string ProductId { get; set; }

    public string ClientId { get; set; }
    public DateOnly Date { get; set; }
    public decimal TotalValue { get; set; }
    public decimal TotalQuantity { get; set; }
}
