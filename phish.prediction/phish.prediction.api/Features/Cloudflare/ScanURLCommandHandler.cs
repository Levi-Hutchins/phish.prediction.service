using MediatR;
using phish.prediction.lib.Configuration;
using System.Text.Json;
using System.Text;

namespace phish.prediction.api.Features.Cloudflare;

public class ScanUrlCommandHandler : IRequestHandler<ScanUrlCommand, string>
{
    private readonly CloudflareConfigService _configService;
    private readonly HttpClient _httpClient;

    public ScanUrlCommandHandler(CloudflareConfigService configService, HttpClient httpClient)
    {
        _configService = configService;
        _httpClient = httpClient;
    }

    public async Task<string> Handle(ScanUrlCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Url);
        var config = _configService.GetConfig();
        Console.WriteLine("client1");

        var payload = JsonSerializer.Serialize(new { url = request.Url });
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
        Console.WriteLine("client2");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.ApiKey}");
        Console.WriteLine("client");

        var response = await _httpClient.PostAsync(config.ScanEndpoint, httpContent, cancellationToken);
        Console.WriteLine(response);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Cloudflare scan failed: {response.StatusCode} - {error}");
        }

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}