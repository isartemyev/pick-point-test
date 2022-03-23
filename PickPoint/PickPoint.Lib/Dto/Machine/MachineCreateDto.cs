using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Machine;

[DataContract]
public class MachineCreateDto
{
    [DataMember, Required]
    public string Number { get; set; }
    
    [DataMember, Required]
    public string Address { get; set; }
}