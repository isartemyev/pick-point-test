using PickPoint.Lib.Dto.Auth;

namespace PickPoint.Lib.Helpers.Auth;

public interface IAuthTokenStore
{
    Task<AuthTokenDto> IssueTokenAsync(AuthLoginDto dto, CancellationToken ct = default);
}