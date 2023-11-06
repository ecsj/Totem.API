using Application.Interfaces;

namespace Infra.Payment;

public class PaymentService : IPaymentService
{
    public bool ProcessPayment(decimal total)
    {
        //TODO: Implementar processo de pagamento
        return true;
    }
}
