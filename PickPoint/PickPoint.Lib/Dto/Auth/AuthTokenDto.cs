using System;
using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Auth
{
    [DataContract]
    public class AuthTokenDto
    {
        [DataMember]
        public string MerchantId { get; set; }

        [DataMember]
        public string AccessToken { get; set; }

        public AuthTokenDto()
        {
            
        }

        public AuthTokenDto(string userId, string accessToken)
        {
            MerchantId = userId ?? throw new ArgumentNullException(nameof(userId));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
        }
    }
}