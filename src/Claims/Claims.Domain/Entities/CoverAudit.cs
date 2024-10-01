using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Claims.Domain.Entities;

public class CoverAudit
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string? CoverId { get; set; }

    public DateTime Created { get; set; }

    public string? HttpRequestType { get; set; }
}