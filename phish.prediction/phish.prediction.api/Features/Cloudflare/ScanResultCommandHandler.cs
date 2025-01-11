using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using phish.prediction.api.Features.Cloudflare.Models;
using phish.prediction.lib.Features.Cloudflare.Config;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace phish.prediction.api.Features.Cloudflare;

public class ScanResultCommandHandler: IRequestHandler<ScanResultCommand, ScanResult>
{
    private readonly CloudflareConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<ScanUrlCommandHandler> _logger;
    private readonly ScanResultService _scanResultService;
    
    public ScanResultCommandHandler(
        IOptions<CloudflareConfiguration> config, 
        HttpClient httpClient, 
        ILogger<ScanUrlCommandHandler> logger,
        ScanResultService service)
    {
        _configuration = config.Value;
        _httpClient = httpClient;
        _logger = logger;
        _scanResultService = service;
    }

    public async Task<ScanResult> Handle(ScanResultCommand request, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration.ApiKey}");
        
        try
        {
            var response = await _httpClient.GetAsync(_configuration.ScanResultEndpoint + request.UUID, cancellationToken);
            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            var scanResult = JsonSerializer.Deserialize<ScanResult>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (scanResult == null)
            {
                _logger.LogError("Failed to parse response from Cloudflare.");
                return new ScanResult()
                {
                    message = "Failed to parse response from Cloudflare." 
                    
                };
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Error response received: {StatusCode} - {JsonResponse}", response.StatusCode, jsonResponse);
            }

            await _scanResultService.CreateAsync(scanResult);
            return scanResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching results: {UUID}", request.UUID);
            return new ScanResult()
            {
                message = "An error occurred while processing the request." 
                
            };
        }
        
    }
    
    
}