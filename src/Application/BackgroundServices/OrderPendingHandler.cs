using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.BackgroundServices;

public class OrderPendingHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderPendingHandler> _logger;

    public OrderPendingHandler(IServiceProvider serviceProvider, ILogger<OrderPendingHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"Waiting for Orders Pending");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var orderUseCase = scope.ServiceProvider.GetRequiredService<IOrderUseCase>();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

            var orders = orderUseCase.GetOrdersPending();

            foreach (var order in orders)
            {
                _logger.LogInformation($"Processing order Id={order.Id} ");

                var result = paymentService.ProcessPayment(order.TotalPrice);

                if (result)
                    await orderUseCase.UpdateOrderStatus(order.Id, OrderStatus.PendingPayment);
            }
            await Task.Delay(1000, stoppingToken);

        }
    }
}