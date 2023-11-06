using Domain.Request;

namespace Application.Interfaces;

public interface IPaymentUseCase
{
    Task<bool> ChangePayment(PaymentRequest request);
    Task UpdateApprovedPayments();
}