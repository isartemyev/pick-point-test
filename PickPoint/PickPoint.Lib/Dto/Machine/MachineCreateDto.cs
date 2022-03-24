using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Machine;

[DataContract]
public class MachineCreateDto
{
    [DataMember, Required, RegularExpression(@"([0-9]{4})[-]([0-9]{3})", ErrorMessage = "Invalid machine number")]
    public string Number { get; set; }
    
    [DataMember, Required]
    public string Address { get; set; }
}