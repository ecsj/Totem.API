using Application.BackgroundServices;
using Application.Interfaces;
using Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public class Dependencies
{
    public static IServiceCollection ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        
        services.AddScoped<IClientUseCase, ClientUseCase>();
        services.AddScoped<IOrderUseCase, OrderUseCase>();
        services.AddScoped<IProductUseCase, ProductUseCase>();
        services.AddScoped<IComboUseCase, ComboUseCase>();
        services.AddScoped<IPaymentUseCase, PaymentUseCase>();

        services.AddHostedService<OrderPendingHandler>();
        services.AddHostedService<PaidOrderHandler>();
        services.AddHostedService<OrderAuthorizedPaymentHandler>();

        return services;
    }
}

