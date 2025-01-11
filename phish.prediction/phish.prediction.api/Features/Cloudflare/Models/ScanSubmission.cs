using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ScanSubmission
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string uuid { get; set; }
    public string api { get; set; }
    public string visibility { get; set; }
    public string url { get; set; }
    public string message { get; set; }



}