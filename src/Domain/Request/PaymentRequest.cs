
using Domain.Entities;

namespace Domain.Request;

public class PaymentRequest
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }
}