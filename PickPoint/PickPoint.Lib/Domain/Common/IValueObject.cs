namespace PickPoint.Lib.Domain.Common;

public interface IValueObject
{
    IEnumerable<object> GetEqualityComponents();
}