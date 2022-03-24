using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using PickPoint.Lib.Extensions;

namespace PickPoint.Lib.Domain.Common;

[BsonIgnoreExtraElements]
[DataContract]
[Serializable]
public abstract class PickPointEntity : IEntity
{
    [DataMember]
    public long CreatedAt { get; protected set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();

    [DataMember]
    public long? UpdatedAt { get; protected set; } = DateTime.UtcNow.ToUnixTimeMilliseconds();

    [DataMember]
    [BsonId]
    public string Id { get; protected set;  } = Guid.NewGuid().ToString("N");
}