using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PickPoint.Lib.Domain.Enums;

namespace PickPoint.Lib.Dto.Merchant
{
    [DataContract]
    public class MerchantCreateDto
    {
        [DataMember]
        [Required(ErrorMessage = "Login is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Login { get; set; }

        [DataMember, Required]
        public EMerchantRole? Role { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DataMember, EmailAddress, Required]
        public string Email { get; set; }
    }
}