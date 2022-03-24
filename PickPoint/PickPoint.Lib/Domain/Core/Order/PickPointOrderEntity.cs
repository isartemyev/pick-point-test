using System.Runtime.Serialization;
using PickPoint.Lib.Domain.Common;
using PickPoint.Lib.Domain.Enums;

namespace PickPoint.Lib.Domain.Core.Order;

[DataContract]
public class PickPointOrderEntity : PickPointEntity
{
    [DataMember]
    public int Number { get; private set; }

    [DataMember] 
    public EOrderStatus Status { get; private set; } = EOrderStatus.Registered;
    
    [DataMember]
    public string[] Items { get; private set; }
    
    [DataMember]
    public decimal Amount { get; private set; }
    
    [DataMember]
    public string MachineNumber { get; private set; }
    
    [DataMember]
    public string RecipientPhone { get; private set; }
    
    [DataMember]
    public string RecipientName { get; private set; }
}