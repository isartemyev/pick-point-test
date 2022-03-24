using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PickPoint.Lib.Validation;

public class OrderItemsMaxAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is IList list)
        {
            return list.Count <= 10;
        }
        
        return false;
    }
}