using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderUseCase
{
    Task<Order> PlaceOrder(OrderRequest order);
    Task<List<Order>> GetOrdersByCustomer(int customerId);
    Task<List<Order>> GetOrdersByCpf(string cpf);
    IQueryable<Order> Get();
    Task<Order> GetById(int orderId);
    IList<Order> GetOrdersPending();
    IList<Order> GetOrdersByStatus(OrderStatus status);
    Task UpdateOrderStatus(int orderId, OrderStatus newStatus);
    Task CancelOrder(int orderId);
}