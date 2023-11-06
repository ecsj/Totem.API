using System.Security.Cryptography.X509Certificates;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories.Base;
using Domain.Request;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases;

public class PaymentUseCase : IPaymentUseCase
{
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IOrderUseCase _orderUseCase;

    public PaymentUseCase(IRepository<Payment> paymentRepository, IOrderUseCase orderUseCase)
    {
        _paymentRepository = paymentRepository;
        _orderUseCase = orderUseCase;
    }

    public async Task<bool> ChangePayment(PaymentRequest request)
    {
        var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);

        if(payment is null) return false;

        payment.ChangeStatus(request.Status);

        await _paymentRepository.UpdateAsync(payment);

        return true;
    }
    public async Task UpdateApprovedPayments()
    {
        var payments = _paymentRepository.Get()
            .Include(x => x.Order)
            .Where(x => x.Status == PaymentStatus.Approved && x.Order.Status == OrderStatus.PendingPayment)
            .ToList();

        foreach (var payment in payments)
        {
            await _orderUseCase.UpdateOrderStatus(payment.OrderId, OrderStatus.AuthorizedPayment);
        }
    }
}

