using Domain.Base;

namespace Domain.Entities;

public class Payment : Entity
{
    public Payment()
    {
            
    }
    public Payment(decimal amount, Order order)
    {
        Amount = amount;
        Currency = "BRL";
        Status = PaymentStatus.Pending;
        PaymentDate = DateTime.UtcNow;
        Order = order;
    }

    public void ChangeStatus(PaymentStatus status) => Status = status;
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime PaymentDate { get; private set; }

    public Order Order { get; private set;  }
    public int OrderId { get; private set; }
}