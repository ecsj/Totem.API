using Domain.Entities;

namespace Application.Interfaces;

public interface IBaseUseCase<TIn, TOut>
{
    IQueryable<TOut> Get();
    Task<TOut> GetById(int id);
    Task<TOut> Add(TIn request);
    Task Update(int id, TIn request);
    Task Delete(int id);
}
