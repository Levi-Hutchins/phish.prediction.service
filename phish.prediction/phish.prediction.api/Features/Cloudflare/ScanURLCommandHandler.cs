using MediatR;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;

using phish.prediction.lib.Features.Cloudflare.Config;

namespace phish.prediction.api.Features.Cloudflare;

public class ScanUrlCommandHandler : IRequestHandler<ScanUrlCommand, ScanSubmission>
{
    private readonly CloudflareConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<ScanUrlCommandHandler> _logger;
    private readonly ScanSubmissionService _scanSubmissionService;

    public ScanUrlCommandHandler(
        IOptions<CloudflareConfiguration> config, 
        HttpClient httpClient, 
        ILogger<ScanUrlCommandHandler> logger,
        ScanSubmissionService service
        )
    {
        _configuration = config.Value;
        _httpClient = httpClient;
        _logger = logger;
        _scanSubmissionService = service;
    }

    public async Task<ScanSubmission> Handle(ScanUrlCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Scanning URL: {Url}", request.Url);

        var payload = JsonSerializer.Serialize(new { url = request.Url });
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration.ApiKey}");

        try
        {
            var response = await _httpClient.PostAsync(_configuration.ScanEndpoint, httpContent, cancellationToken);
            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
            
            var scanResult = JsonSerializer.Deserialize<ScanSubmission>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (scanResult == null)
            {
                _logger.LogError("Failed to parse response from Cloudflare.");
                return new ScanSubmission
                {
                    message = "Failed to parse response from Cloudflare." 
                    
                };
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Error response received: {StatusCode} - {JsonResponse}", response.StatusCode, jsonResponse);
            }

            await _scanSubmissionService.CreateAsync(scanResult);
            
            return scanResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while scanning the URL: {URL}", request.Url);
            return new ScanSubmission
            {
                message = "An error occurred while processing the request." 
                
            };
        }
    }
}
