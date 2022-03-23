using System.Runtime.Serialization;
using PickPoint.Lib.Domain.Common;

namespace PickPoint.Lib.Domain.Core.Machine;

[DataContract]
public class PickPointMachineEntity : PickPointEntity
{
    [DataMember]
    public string Number { get; private set; }
    
    [DataMember]
    public string Address { get; private set; }
    
    [DataMember]
    public bool Enabled { get; private set; }
}