using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PickPoint.Lib.Domain.Common;

[DataContract, Serializable]
public abstract class PickPointValueObject: IValueObject
{
    public abstract IEnumerable<object> GetEqualityComponents();

    private bool Equals(PickPointValueObject other) => GetHashCode() == other.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((PickPointValueObject)obj);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 397 ^ (obj?.GetHashCode() ?? 0);
                }
            });
    }

    public static bool operator ==(PickPointValueObject first, PickPointValueObject second) => !(first is null) && !(second is null) && first.Equals(second);

    public static bool operator !=(PickPointValueObject first, PickPointValueObject second) => !(first == second);
}