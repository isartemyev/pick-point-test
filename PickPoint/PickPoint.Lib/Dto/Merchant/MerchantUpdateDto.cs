using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Merchant;

[DataContract]
public class MerchantUpdateDto
{
    [DataMember, Required]
    public string Id { get; set; }
        
    [DataMember, Required]
    public string Name { get; set; }

    [DataMember, EmailAddress, Required(AllowEmptyStrings = true)]
    public string Email { get; set; }
}