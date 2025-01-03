using MediatR;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;
using phish.prediction.lib.Features.Cloudflare.Config;

namespace phish.prediction.api.Features.Cloudflare;

public class ScanUrlCommandHandler : IRequestHandler<ScanUrlCommand, string>
{
    private readonly CloudflareConfig _config;
    private readonly HttpClient _httpClient;

    public ScanUrlCommandHandler(IOptions<CloudflareConfig> config, HttpClient httpClient)
    {
        _config = config.Value;
        _httpClient = httpClient;
    }

    public async Task<string> Handle(ScanUrlCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Url);
        Console.WriteLine("client1");

        var payload = JsonSerializer.Serialize(new { url = request.Url });
        var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
        Console.WriteLine("client2");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config.ApiKey}");
        Console.WriteLine("client");

        var response = await _httpClient.PostAsync(_config.ScanEndpoint, httpContent, cancellationToken);
        Console.WriteLine(response);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Cloudflare scan failed: {response.StatusCode} - {error}");
        }

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}