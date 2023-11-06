using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.BackgroundServices;

public class OrderAuthorizedPaymentHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderAuthorizedPaymentHandler> _logger;

    public OrderAuthorizedPaymentHandler(IServiceProvider serviceProvider, ILogger<OrderAuthorizedPaymentHandler> logger)
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

            var paymentUseCase = scope.ServiceProvider.GetRequiredService<IPaymentUseCase>();

            await paymentUseCase.UpdateApprovedPayments();
            
            await Task.Delay(1000, stoppingToken);

        }
    }
}
