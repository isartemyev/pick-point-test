using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PickPoint.Lib.Domain.Enums;

namespace PickPoint.Lib.Dto.Order
{
    [DataContract]
    public class OrderUpdateDto
    {
        [DataMember, Required]
        public string Id { get; set; }

        [DataMember, Required]
        public EOrderStatus? Status { get; set; }

        [DataMember]
        public int Number { get; set; }
        
        [DataMember]
        public string[] Items { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }
        
        [DataMember]
        public string MachineNumber { get; set; }

        [DataMember]
        public string RecipientPhone { get; set; }

        [DataMember]
        public string RecipientName { get; set; }
    }
}