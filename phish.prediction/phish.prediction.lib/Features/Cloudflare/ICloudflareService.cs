namespace phish.prediction.lib.Features.Cloudflare;

public interface ICloudflareService
{
    public Task<string> ScanURL(string urlToScan);
}