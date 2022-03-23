using System.Collections.Generic;

namespace PickPoint.Lib.Domain.Common;

public interface IValueObject
{
    IEnumerable<object> GetEqualityComponents();
}