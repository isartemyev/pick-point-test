using System.Runtime.Serialization;

namespace PickPoint.Lib.Dto.Auth;

[DataContract]
public class AuthTokenDto
{
    [DataMember]
    public string MerchantId { get; set; }

    [DataMember]
    public string AccessToken { get; set; }

    public AuthTokenDto(string merchantId, string accessToken)
    {
        MerchantId = merchantId ?? throw new ArgumentNullException(nameof(merchantId));
        AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
    }
}