using Application.Interfaces;
using Domain.Entities;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientUseCase _clientUseCase;

    public ClientController(IClientUseCase clientUseCase)
    {
        _clientUseCase = clientUseCase;
    }

    [HttpGet]
    public ActionResult<IQueryable<Client>> Get()
    {
        var clients = _clientUseCase.Get();
        return Ok(clients);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Client>> GetById(int id)
    {
        var client = await _clientUseCase.GetById(id);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }


    [HttpGet("{cpf}")]
    public async Task<ActionResult<Client>> GetByCpf(string cpf)
    {
        var client = await _clientUseCase.GetByCpf(cpf);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }
    
    [HttpPost]
    public async Task<ActionResult<Client>> Create([FromBody] ClientRequest request)
    {
        var client = await _clientUseCase.Add(request);

        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClientRequest updatedClient)
    {
        await _clientUseCase.Update(id, updatedClient);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        await _clientUseCase.Delete(id);
        return NoContent();
    }
}
