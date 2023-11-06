using Domain.Entities;

namespace Domain.Request;

public record struct ComboRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<ProductRequest> Products { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    public string ImageURL { get; set; }
}