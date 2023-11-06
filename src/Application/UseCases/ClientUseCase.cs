﻿using Application.Interfaces;
using Domain.Base;
using Domain.Entities;
using Domain.Repositories.Base;
using Domain.Request;

namespace Application.UseCases;

public class ClientUseCase : IClientUseCase
{
    private readonly IClientRepository _clienteRepository;

    public ClientUseCase(IClientRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public IQueryable<Client> Get()
    {
        return _clienteRepository.Get();
    }

    public async Task<Client> GetById(int id)
    {
        return await _clienteRepository.GetByIdAsync(id);
    }

    public async Task<Client> GetByCpf(string cpf)
    {
        return await _clienteRepository.GetByCpf(cpf);
    }
    public async Task<Client> Add(ClientRequest request)
    {
        var client = Client.FromRequest(request);

        await _clienteRepository.AddAsync(client);

        return client;
    }

    public async Task Update(int id, ClientRequest clientRequest)
    {
        var client = await GetById(id);

        if (client is null) throw new DomainException("Client não encontrado");

        await _clienteRepository.UpdateAsync(client);
    }

    public async Task Delete(int id)
    {
        var client = await GetById(id);

        if (client is null) throw new DomainException("Client não encontrado");

        await _clienteRepository.DeleteAsync(client);
    }
}