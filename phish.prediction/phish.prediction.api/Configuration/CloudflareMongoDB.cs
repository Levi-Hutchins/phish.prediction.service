namespace phish.prediction.lib.Features.Cloudflare.Config;

public class CloudflareMongoDB
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    
    public Dictionary<string, string> Collections { get; set; } 

}