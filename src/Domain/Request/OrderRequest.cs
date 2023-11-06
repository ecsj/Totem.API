namespace Domain.Entities;

public record struct OrderRequest
{
    public int? ClientId { get; set; }
    public List<OrderProductsRequest> Products { get; set; }
    public decimal Total { get; set; }
}
