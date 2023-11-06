namespace Application.Interfaces;

public interface IPaymentService
{
    bool ProcessPayment(decimal total);
}
