using MediatR;
using Microsoft.AspNetCore.Mvc;
using phish.prediction.api.Features.Cloudflare;
using phish.prediction.api.Features.Cloudflare.Models;
using phish.prediction.api.Models;

namespace phish.prediction.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CloudflareController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CloudflareController> _logger;

    public CloudflareController(IMediator mediator, ILogger<CloudflareController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("scan-url")]
    public async Task<IActionResult> ScanUrl([FromBody] ScanUrlRequest request)
    {
        var result = await _mediator.Send(new ScanUrlCommand(request.Url));
        _logger.LogInformation(result);
        return Ok(result);
    }
}

