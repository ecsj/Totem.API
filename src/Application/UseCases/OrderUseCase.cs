using Application.Interfaces;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public class OrderUseCase : IOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IClientUseCase _clientUseCase;
    private readonly ILogger<OrderUseCase> _logger;

    public OrderUseCase(IOrderRepository orderRepository,
                        IRepository<Product> productRepository,
                        IClientUseCase clientUseCase,
                        ILogger<OrderUseCase> logger)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _clientUseCase = clientUseCase;
        _logger = logger;
    }

    public async Task<Order> PlaceOrder(OrderRequest request)
    {
        try
        {
            var order = Order.FromOrderRequest(request);

            foreach (var item in order.Products)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product is null)
                    throw new DomainException("Produto não encontrado");

            }

            if (order.ClientId is not null)
            {
                var client = await _clientUseCase.GetById((int)order.ClientId);

                if(client is null) 
                    throw new DomainException("Cliente não encontrado");

            }

            await _orderRepository.AddAsync(order);

            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao criar Pedido", ex);
            throw;
        }
    }

    public IQueryable<Order> Get()
    {
        return _orderRepository.Get();
    }
    public IList<Order> GetOrdersByStatus(OrderStatus status)
    {
        return _orderRepository.GetAll().Where(o => o.Status == status).ToList();
    }
    public IList<Order> GetOrdersPending()
    {
        return _orderRepository.GetAll().Where(o => o.Status == OrderStatus.Pending).ToList();
    }

    public async Task<List<Order>> GetOrdersByCustomer(int customerId)
    {
        return await _orderRepository.GetOrdersByCustomerId(customerId);
    }

    public async Task<List<Order>> GetOrdersByCpf(string cpf)
    {
        return await _orderRepository.GetOrdersByCpf(cpf);
    }
    public async Task<Order> GetById(int orderId)
    {
        return await _orderRepository.GetByIdAsync(orderId);
    }

    public async Task UpdateOrderStatus(int orderId, OrderStatus newStatus)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        order.ChangeStatus(newStatus);

        await _orderRepository.UpdateAsync(order);
    }

    public async Task CancelOrder(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        order.ChangeStatus(OrderStatus.Canceled);

        await _orderRepository.UpdateAsync(order);
    }
}