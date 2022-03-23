using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Machine;

[DataContract]
public class MachineUpdateDto
{
    [DataMember, Required]
    public string Id { get; set; }
    
    [DataMember, Required]
    public string Number { get; set; }
    
    [DataMember, Required]
    public string Address { get; set; }
    
    [DataMember, Required]
    public bool? Enabled { get; set; }
}