using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Machine;

[DataContract]
public class MachineFilterDto
{
    [DataMember]
    public string[] Ids { get; set; }
        
    [DataMember]
    public string Text { get; set; }
        
    [DataMember]
    public long? PeriodStart { get; set; }
        
    [DataMember]
    public long? PeriodEnd { get; set; }
        
    [DataMember]
    public bool? Enabled { get; set; }
}