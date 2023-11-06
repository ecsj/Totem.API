using Domain.Base;

namespace Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    public Order() { }
    public Order(int? clientId, List<OrderProduct> products, OrderStatus status, decimal totalPrice)
    {
        ClientId = clientId;
        OrderDate = DateTime.UtcNow;
        Products = products;
        Status = status;
        TotalPrice = totalPrice;
        Payment = new Payment(totalPrice, this);

        Validate();
    }

    public Client? Client { get;  }
    public int? ClientId { get; }

    public Payment Payment { get; set; }

    public DateTime OrderDate { get; }
    public List<OrderProduct> Products { get; } = new List<OrderProduct>();
    public OrderStatus Status { get; private set; }
    public string OrderStatusDescription => Status.GetDescription();
    public TimeSpan WaitingTime => DateTime.UtcNow - OrderDate;
    public decimal TotalPrice { get; set; }

    public void ChangeStatus(OrderStatus status)
    {
        Status = status;
    }
    public static Order FromOrderRequest(OrderRequest orderRequest)
    {
        var products = new List<OrderProduct>();
        foreach (var orderProductRequest in orderRequest.Products)
        {
            var product = new OrderProduct
            {
                ProductId = orderProductRequest.ProductId,
                Quantity = orderProductRequest.Quantity,
                Total = orderProductRequest.Total,
                Comments = orderProductRequest.Comments,
                Additional = new List<Additional>()
            };

            foreach (var additionalRequest in orderProductRequest.Additional)
            {
                var additional = new Additional
                {
                    ProductId = additionalRequest.ProductId,
                    Price = additionalRequest.AdditionalPrice
                };
                product.Additional.Add(additional);
            }

            products.Add(product);
        }

        var order = new Order(orderRequest.ClientId, products, OrderStatus.Pending, orderRequest.Total);

        return order;
    }

    public void Validate()
    {
        if (Products == null || Products.Count == 0)
        {
            throw new Exception("O pedido não contém nenhum produto.");
        }

        foreach (var product in Products)
        {
            if (product.Quantity <= 0)
            {
                throw new Exception($"A quantidade para o produto '{product.ProductId}' deve ser maior que zero.");
            }
        }

        if (TotalPrice <= 0)
        {
            throw new Exception("O preço total do pedido deve ser maior que zero.");
        }
    }
}