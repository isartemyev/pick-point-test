using System.Runtime.Serialization;
using PickPoint.Lib.Domain.Enums;

namespace PickPoint.Lib.Dto.Order;

[DataContract]
public class OrderFilterDto
{
    [DataMember]
    public string[] Ids { get; set; }
        
    [DataMember]
    public string Text { get; set; }
        
    [DataMember]
    public long? PeriodStart { get; set; }
        
    [DataMember]
    public long? PeriodEnd { get; set; }
        
    [DataMember]
    public EOrderStatus? Status { get; set; }
}