using MediatR;

namespace phish.prediction.api.Features.Cloudflare;

public class ScanUrlCommand : IRequest<string>
{
    public string Url { get; set; }

    public ScanUrlCommand(string url)
    {
        Url = url;
    }
}