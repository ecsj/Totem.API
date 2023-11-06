using Application.Interfaces;
using Domain.Entities;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductUseCase _productUseCase;

    public ProductController(IProductUseCase productUseCase)
    {
        _productUseCase = productUseCase;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var products = _productUseCase.Get();

        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productUseCase.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    [HttpGet("GetCategory/{category}")]
    public IActionResult GetCategory(Category category)
    {
        var products = _productUseCase.GetByCategory(category);

        return Ok(products);
    }

    [HttpGet("GetProductCategories/")]
    public IActionResult GetProductCategories()
    {
        var categories = new Dictionary<int, string>();

        Enum.GetValues(typeof(Category))
                        .Cast<Category>()
                        .ToList().ForEach(v => categories.Add(Convert.ToInt32(v), v.ToString()));

        return Ok(categories);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRequest request)
    {
        var product = await _productUseCase.Add(request);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductRequest updatedProduct)
    {
        await _productUseCase.Update(id, updatedProduct);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productUseCase.Delete(id);

        return NoContent();
    }
}