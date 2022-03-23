using PickPoint.Lib.Dto.Auth;
using PickPoint.Lib.Facades.Declarations;
using PickPoint.Lib.Helpers.Auth;

namespace PickPoint.Lib.Facades.Implementations;

public class AuthFacade : IAuthFacade
{
    private readonly IAuthTokenStore _tokenStore;

    public AuthFacade(IAuthTokenStore tokenStore)
    {
        _tokenStore = tokenStore ?? throw new ArgumentNullException(nameof(tokenStore));
    }

    public async Task<AuthTokenDto> LoginAsync(AuthLoginDto payload, CancellationToken token = default)
    {
        return await _tokenStore.IssueTokenAsync(payload, token);
    }
}