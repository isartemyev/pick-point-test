using System.Runtime.Serialization;
using PickPoint.Lib.Domain.Common;
using PickPoint.Lib.Domain.Enums;

namespace PickPoint.Lib.Domain.Core.Merchant;

[DataContract]
public class PickPointMerchantEntity : PickPointEntity
{
    [DataMember]
    public string Name { get; private set; }

    [DataMember]
    public string Login { get; private set; }

    [DataMember]
    public string PasswordHash { get; private set; }

    [DataMember]
    public EMerchantRole Role { get; private set; }

    [DataMember]
    public string Email { get; private set; }
}