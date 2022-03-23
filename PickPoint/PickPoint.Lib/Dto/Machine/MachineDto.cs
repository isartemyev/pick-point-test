using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Machine;

[DataContract]
public class MachineDto
{
    [DataMember]
    public string Id { get; set; }
        
    [DataMember]
    public long CreatedAt { get; set; }

    [DataMember]
    public long UpdatedAt { get; set; }
    
    [DataMember]
    public string Number { get; set; }
    
    [DataMember]
    public string Address { get; set; }
    
    [DataMember]
    public bool Enabled { get; set; }
}