using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PickPoint.Lib.Validation;

namespace PickPoint.Lib.Dto.Order;

[DataContract]
public class OrderUpdateDto
{
    [DataMember, Required]
    public string Id { get; set; }

    [DataMember]
    public int Number { get; set; }
        
    [DataMember, OrderItemsMax(ErrorMessage = "Invalid items count")]
    public string[] Items { get; set; }

    [DataMember]
    public decimal? Amount { get; set; }

    [DataMember, RegularExpression(@"[+][7]([0-9]{3})[-]([0-9]{3})[-]([0-9]{2})[-]([0-9]{2})", ErrorMessage = "Invalid phone number")]
    public string RecipientPhone { get; set; }

    [DataMember]
    public string RecipientName { get; set; }
}