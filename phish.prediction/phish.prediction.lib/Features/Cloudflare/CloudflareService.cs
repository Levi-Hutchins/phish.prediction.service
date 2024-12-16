using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using phish.prediction.lib.Features.Cloudflare.Config;

namespace phish.prediction.lib.Features.Cloudflare;

public class CloudflareService: ICloudflareService
{
    private readonly CloudflareConfig _config;

    public CloudflareService(CloudflareService config)
    {

        _config = _config;
    }
    public async Task<string> ScanURL(string urlToScan)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _config.ApiKey);
        var payload = new
        {
            url = urlToScan
        };
        var request = JsonSerializer.Serialize(new {
            url = urlToScan
        });

        // Send the POST request
        try
        {
            var response = await httpClient.PostAsync(_config.ApiKey, new StringContent(request, Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("URL scanning initiated successfully!");
                Console.WriteLine("Response: " + responseContent);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine("Response: " + responseContent);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        return "t";

    }

    
    
}