using Domain.Base;
using Domain.Request;

namespace Domain.Entities;

public class Combo : Entity, IAggregateRoot
{

    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public string Description { get; set; }
    public Category Category { get; set; }
    public string ImageURL { get; set; }

    public Combo()
    {
            
    }

    public static Combo FromRequest(ComboRequest request)
    {
        throw new NotImplementedException();
    }
}
