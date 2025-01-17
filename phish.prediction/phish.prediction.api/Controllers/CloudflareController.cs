using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [ProducesResponseType(typeof(ScanSubmission), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ScanUrl([FromBody] ScanUrlRequest request)
    {
        try
        {
            var scanSubmission = await _mediator.Send(new ScanUrlCommand(request.Url));
            return Ok(scanSubmission);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occured - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
        }
        
    }

    [HttpGet("scan-results/{uuid}")]
    [ProducesResponseType(typeof(ScanResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetScanResult(string uuid)
    {
        if (!Guid.TryParse(uuid, out _))
            return BadRequest("Invalid ID");
        try
        {
            var scanResult = await _mediator.Send(new ScanResultCommand(uuid));
            return Ok(scanResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occured - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
        }
    }
    
}

