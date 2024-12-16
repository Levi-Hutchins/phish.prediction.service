using MediatR;
using Microsoft.AspNetCore.Mvc;
using phish.prediction.api.Features.Cloudflare;
using phish.prediction.api.Features.Cloudflare.Models;

namespace phish.prediction.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CloudflareController : ControllerBase
{
    private readonly IMediator _mediator;

    public CloudflareController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("scan")]
    public async Task<IActionResult> ScanUrl([FromBody] ScanUrlRequest request)
    {
        var result = await _mediator.Send(new ScanUrlCommand(request.Url));
        return Ok(result);
    }
}

