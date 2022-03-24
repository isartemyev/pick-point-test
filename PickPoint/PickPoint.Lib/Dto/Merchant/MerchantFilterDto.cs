using System.Runtime.Serialization;
using PickPoint.Lib.Domain.Enums;

namespace PickPoint.Lib.Dto.Merchant;

[DataContract]
public class MerchantFilterDto
{
    [DataMember]
    public string[]? Ids { get; set; }
        
    [DataMember]
    public string? Text { get; set; }
        
    [DataMember]
    public long? PeriodStart { get; set; }
        
    [DataMember]
    public long? PeriodEnd { get; set; }
        
    [DataMember]
    public EMerchantRole? Role { get; set; }
}