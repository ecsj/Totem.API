using Application.Interfaces;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories.Base;
using Domain.Request;

namespace Application.UseCases;

public class ComboUseCase : IComboUseCase
{
    private readonly IRepository<Combo> _comboRepository;

    public ComboUseCase(IRepository<Combo> comboRepository)
    {
        _comboRepository = comboRepository;
    }

    public IQueryable<Combo> Get()
    {
        return _comboRepository.Get();
    }

    public async Task<Combo> GetById(int productId)
    {
        return await _comboRepository.GetByIdAsync(productId);
    }

    public async Task<Combo> Add(ComboRequest request)
    {
        var combo = Combo.FromRequest(request);

        await _comboRepository.AddAsync(combo);

        return combo;
    }

    public async Task Update(int id, ComboRequest request)
    {
        var combo = await _comboRepository.GetByIdAsync(id);
        if (combo != null)
        {
            await _comboRepository.UpdateAsync(combo);
        }

        _comboRepository.UpdateAsync(combo);
    }

    public async Task Delete(int productId)
    {
        var combo = await GetById(productId);

        if (combo is null) throw new DomainException("Produto não encontrado");

        await _comboRepository.DeleteAsync(combo);
    }
}