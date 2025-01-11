using MediatR;
using phish.prediction.api.Features.Cloudflare.Models;
using phish.prediction.api.Models;

namespace phish.prediction.api.Features.Cloudflare;

public class ScanUrlCommand : IRequest<ScanSubmission>
{
    public string Url { get; set; }

    public ScanUrlCommand(string url)
    {
        Url = url;
    }
}