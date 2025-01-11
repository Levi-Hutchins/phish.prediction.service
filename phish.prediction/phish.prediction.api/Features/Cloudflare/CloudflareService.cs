using Microsoft.Extensions.Options;
using MongoDB.Driver;
using phish.prediction.lib.Features.Cloudflare.Config;

namespace phish.prediction.api.Features.Cloudflare;

public class CloudflareService
{
    private readonly IMongoCollection<ScanSubmission> _submissionCollection;

    public CloudflareService(IOptions<CloudflareMongoDB> mongoSettings)
    {
        var mongoClient = new MongoClient(
            mongoSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoSettings.Value.DatabaseName);
        
        _submissionCollection = mongoDatabase.GetCollection<ScanSubmission>(
            mongoSettings.Value.CollectionName);
    }
    public async Task<List<ScanSubmission>> GetAsync() =>
        await _submissionCollection.Find(_ => true).ToListAsync();

    public async Task<ScanSubmission?> GetAsync(string id) =>
        await _submissionCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ScanSubmission newBook) =>
        await _submissionCollection.InsertOneAsync(newBook);
    
}