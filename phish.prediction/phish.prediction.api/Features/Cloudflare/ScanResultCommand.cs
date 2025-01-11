using MediatR;
using phish.prediction.api.Features.Cloudflare.Models;


namespace phish.prediction.api.Features.Cloudflare;

public class ScanResultCommand : IRequest<ScanResult>
{
    public string UUID { get; set; }

    public ScanResultCommand(string uuid)
    {
        UUID = uuid;
    }
}