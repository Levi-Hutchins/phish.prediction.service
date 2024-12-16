using Microsoft.Extensions.Configuration;
using phish.prediction.lib.Features.Cloudflare.Config;

namespace phish.prediction.lib.Configuration;

public class CloudflareConfigService
{
    private readonly CloudflareConfig _config;
    public CloudflareConfigService(IConfiguration configuration)
    {
        _config = configuration.GetSection("Cloudflare").Get<CloudflareConfig>();
    }
    public CloudflareConfig GetConfig() => _config;


}