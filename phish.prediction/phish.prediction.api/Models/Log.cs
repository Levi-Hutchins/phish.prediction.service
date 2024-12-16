namespace phish.prediction.api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Log
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string RequestMethod { get; set; }
    public string Endpoint { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string IPAddress { get; set; }
}