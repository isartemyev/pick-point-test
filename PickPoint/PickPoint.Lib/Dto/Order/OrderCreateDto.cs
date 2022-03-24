using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PickPoint.Lib.Validation;

namespace PickPoint.Lib.Dto.Order;

[DataContract]
public class OrderCreateDto
{
    [DataMember, Required]
    public int? Number { get; set; }
        
    [DataMember, Required, OrderItemsMax(ErrorMessage = "Items count is not valid")]
    public string[] Items { get; set; }

    [DataMember, Required]
    public decimal? Amount { get; set; }
        
    [DataMember, Required, RegularExpression(@"([0-9]{4})[-]([0-9]{3})", ErrorMessage = "Invalid machine number")]
    public string MachineNumber { get; set; }

    [DataMember, Required, RegularExpression(@"[+][7]([0-9]{3})[-]([0-9]{3})[-]([0-9]{2})[-]([0-9]{2})", ErrorMessage = "Invalid phone number")]
    public string RecipientPhone { get; set; }

    [DataMember, Required]
    public string RecipientName { get; set; }
}