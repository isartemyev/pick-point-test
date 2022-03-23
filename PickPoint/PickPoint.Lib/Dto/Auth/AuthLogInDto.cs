using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Auth
{
    [DataContract]
    public class AuthLogInDto
    {
        [DataMember]
        [Required(ErrorMessage = "Login is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Login { get; set; }
        
        [DataMember]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}