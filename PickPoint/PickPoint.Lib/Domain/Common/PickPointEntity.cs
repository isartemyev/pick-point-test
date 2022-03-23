using System;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace PickPoint.Lib.Domain.Common;

[BsonIgnoreExtraElements]
[DataContract]
[Serializable]
public abstract class PickPointEntity : IEntity
{
    [DataMember]
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    [DataMember]
    public DateTime? UpdatedAt { get; protected set; }

    [DataMember]
    [BsonId]
    public string Id { get; protected set;  } = Guid.NewGuid().ToString("N");
}