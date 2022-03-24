using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Order;

[DataContract]
public class OrderCreateDto
{
    [DataMember, Required]
    public int? Number { get; set; }
        
    [DataMember, Required]
    public string[] Items { get; set; }

    [DataMember, Required]
    public decimal? Amount { get; set; }
        
    [DataMember, Required]
    public string MachineNumber { get; set; }

    [DataMember, Required]
    public string RecipientPhone { get; set; }

    [DataMember, Required]
    public string RecipientName { get; set; }
}