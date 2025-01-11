using MediatR;

namespace phish.prediction.api.Features.Cloudflare;

public class ScanUrlCommand : IRequest<ScanSubmission>
{
    public string Url { get; set; }

    public ScanUrlCommand(string url)
    {
        Url = url;
    }
}