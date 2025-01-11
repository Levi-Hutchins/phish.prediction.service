using Microsoft.Extensions.Options;
using MongoDB.Driver;
using phish.prediction.api.Features.Cloudflare.Models;
using phish.prediction.lib.Features.Cloudflare.Config;
using Task = System.Threading.Tasks.Task;

namespace phish.prediction.api.Features.Cloudflare;

public class ScanResultService : BaseMongoService
{
    public ScanResultService(IOptions<CloudflareMongoDB> mongoSettings) : base(mongoSettings)
    {
    }

    public async Task<List<ScanResult>> FindAllAsync()
    {
        return await GetCollection<ScanResult>("scan-results")
            .Find(_ => true).ToListAsync();
    }

    public async Task<ScanResult?> FindByIdAsync(string uuid)
    {
        return await GetCollection<ScanResult>("scan-results")
            .Find(x => x.task.uuid == uuid).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(ScanResult newSubmission)
    {
        await GetCollection<ScanResult>("scan-results")
            .InsertOneAsync(newSubmission);
    }
}