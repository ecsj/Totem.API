using Application.Interfaces;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentWebhookController : ControllerBase
{
    private readonly IPaymentUseCase _paymentUse;

    public PaymentWebhookController(IPaymentUseCase paymentUse)
    {
        _paymentUse = paymentUse;
    }

    [HttpPost("status")] // Rota para receber o status do pagamento
    public async Task<IActionResult> ReceivePaymentStatus([FromBody] PaymentRequest request)
    {
        var result = await _paymentUse.ChangePayment(request);

        if(result)
            return Ok("Status de pagamento recebido com sucesso");
        return BadRequest("Pedido não processado");
    }
}
