using Microsoft.Extensions.Options;
using MongoDB.Driver;
using phish.prediction.lib.Features.Cloudflare.Config;

namespace phish.prediction.api.Features.Cloudflare;

public class ScanSubmissionService : BaseMongoService
{
    public ScanSubmissionService(IOptions<CloudflareMongoDB> mongoSettings) : base(mongoSettings)
    {
    }

    public async Task<List<ScanSubmission>> FindAllAsync()
    {
        return await GetCollection<ScanSubmission>("scan-submissions")
            .Find(_ => true).ToListAsync();
    }

    public async Task<ScanSubmission?> FindByIdAsync(string uuid)
    {
        return await GetCollection<ScanSubmission>("scan-submissions")
            .Find(x => x.uuid == uuid).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(ScanSubmission newSubmission)
    {
         await GetCollection<ScanSubmission>("scan-submissions")
             .InsertOneAsync(newSubmission);
    }
}