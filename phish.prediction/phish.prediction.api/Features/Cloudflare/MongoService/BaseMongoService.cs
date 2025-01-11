using Microsoft.Extensions.Options;
using MongoDB.Driver;
using phish.prediction.lib.Features.Cloudflare.Config;

namespace phish.prediction.api.Features.Cloudflare;

public abstract class BaseMongoService
{
    private readonly IMongoDatabase _database;
    private readonly CloudflareMongoDB _config;

    protected BaseMongoService(IOptions<CloudflareMongoDB> mongoSettings)
    {
        _config = mongoSettings.Value;

        var client = new MongoClient(_config.ConnectionString);
        _database = client.GetDatabase(_config.DatabaseName);
    }

    protected IMongoCollection<T> GetCollection<T>(string logicalName)
    {
        if (!_config.Collections.TryGetValue(logicalName, out var collectionName))
        {
            throw new KeyNotFoundException($"Collection with logical name '{logicalName}' not found in the configuration.");
        }

        return _database.GetCollection<T>(collectionName);
    }
}