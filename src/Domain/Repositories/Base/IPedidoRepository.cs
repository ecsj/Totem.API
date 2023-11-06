using Domain.Entities;

namespace Domain.Repositories.Base;

public interface IOrderRepository : IRepository<Order>
{
    IQueryable<Order> GetAll();
    Task<List<Order>> GetOrdersByCustomerId(int customerId);
    Task<List<Order>> GetOrdersByCpf(string cpf);
}