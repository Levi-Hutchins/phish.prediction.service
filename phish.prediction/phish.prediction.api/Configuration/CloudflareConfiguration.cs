namespace phish.prediction.lib.Features.Cloudflare.Config;

public class CloudflareConfiguration
{
    public required string ScanEndpoint { get; set; }
    public required string ScanResultEndpoint { get; set; }
    public required string ApiKey { get; set; }
}