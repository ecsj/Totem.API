using Application.Interfaces;
using Domain.Entities;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ComboController : ControllerBase
{
    private readonly IComboUseCase _comboUseCase;

    public ComboController(IComboUseCase comboUseCase)
    {
        _comboUseCase = comboUseCase;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Combo>> Get()
    {
        var products = _comboUseCase.Get();

        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _comboUseCase.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ComboRequest request)
    {
        var product = await _comboUseCase.Add(request);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ComboRequest request)
    {
        await _comboUseCase.Update(id, request);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _comboUseCase.Delete(id);

        return NoContent();
    }
}