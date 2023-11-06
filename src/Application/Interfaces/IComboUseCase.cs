using Domain.Entities;
using Domain.Request;

namespace Application.Interfaces;

public interface IComboUseCase : IBaseUseCase<ComboRequest, Combo>
{ }