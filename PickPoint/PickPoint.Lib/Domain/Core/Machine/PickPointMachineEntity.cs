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

    public PickPointMachineEntity()
    {
    }

    public PickPointMachineEntity(string number, string address, bool enabled)
    {
        Number = number ?? throw new ArgumentNullException(nameof(number));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Enabled = enabled;
    }
}