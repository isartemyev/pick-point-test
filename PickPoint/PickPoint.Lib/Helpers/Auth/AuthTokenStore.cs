using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Domain.Exceptions;
using PickPoint.Lib.Dto.Auth;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.Lib.Helpers.Auth;

public class AuthTokenStore : IAuthTokenStore
{
    public const string Issuer = "PickPointAuthServer";
    public const string Audience = "https://pickpoint.ru/";
    private const string Key = "sdfsf#H&R*cr+sdfsdfvc*j7";

    private readonly IMerchantRepository _merchantRepository;

    public AuthTokenStore(IMerchantRepository merchantRepository)
    {
        _merchantRepository = merchantRepository ?? throw new ArgumentNullException(nameof(merchantRepository));
    }

    public async Task<AuthTokenDto> IssueTokenAsync(AuthLoginDto dto, CancellationToken ct = default)
    {
        var merchant  = (await _merchantRepository.FilterAsync(item =>
            item.Login == dto.Login &&
            PasswordHasher.Verify(dto.Password, item.PasswordHash), ct)).FirstOrDefault();

        if (merchant is null)
        {
            throw new PickPointEntityNotFoundException("Merchant not found by credentials");
        }

        return Create(merchant.Id, merchant.Role);
    }
    
    private static AuthTokenDto Create(string id, EMerchantRole role)
    {
        var lifetime = role switch
        {
            EMerchantRole.Admin => 60,
            EMerchantRole.Merchant => 525600,
            EMerchantRole.None => throw new ArgumentOutOfRangeException(),
            _ => throw new ArgumentOutOfRangeException()
        };

        var accessExpiredIn = DateTime.UtcNow + TimeSpan.FromMinutes(lifetime);

        var accessToken = GenerateAccessToken(id, accessExpiredIn);

        return new AuthTokenDto(id, accessToken);
    }

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }

    private static string GenerateAccessToken(string merchantId, DateTime expiredIn)
    {
        var claims = new List<Claim> { new(ClaimsIdentity.DefaultIssuer, merchantId) };

        var identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        var jwt = new JwtSecurityToken(Issuer,
            Audience,
            notBefore: DateTime.UtcNow,
            claims: identity.Claims,
            expires: expiredIn,
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}