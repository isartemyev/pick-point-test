using System.Runtime.Serialization;
using PickPoint.Lib.Domain.Enums;

namespace PickPoint.Lib.Dto.Order
{
    [DataContract]
    public class OrderDto
    {
        [DataMember]
        public string Id { get; set; }
        
        [DataMember]
        public long CreatedAt { get; set; }

        [DataMember]
        public long UpdatedAt { get; set; }
        
        [DataMember]
        public int Number { get; set; }

        [DataMember]
        public EOrderStatus Status { get; set; }

        [DataMember]
        public string[] Items { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
        
        [DataMember]
        public string MachineNumber { get; set; }

        [DataMember]
        public string RecipientPhone { get; set; }

        [DataMember]
        public string RecipientName { get; set; }
    }
}