namespace Domain.Entities;

public class Additional
{
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
}
