using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Claims.Domain.Entities;

public class ClaimAudit
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string? ClaimId { get; set; }

    public DateTime Created { get; set; }

    public string? HttpRequestType { get; set; }
}