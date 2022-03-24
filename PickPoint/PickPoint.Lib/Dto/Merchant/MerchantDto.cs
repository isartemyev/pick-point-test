using System.Runtime.Serialization;
using PickPoint.Lib.Domain.Enums;

namespace PickPoint.Lib.Dto.Merchant;

[DataContract]
public class MerchantDto
{
    [DataMember]
    public string Id { get; set; }
        
    [DataMember]
    public long CreatedAt { get; set; }

    [DataMember]
    public long UpdatedAt { get; set; }
        
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Login { get; set; }

    [DataMember]
    public EMerchantRole Role { get; set; }

    [DataMember]
    public string Email { get; set; }
}