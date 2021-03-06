using PickPoint.Lib.Dto.Auth;

namespace PickPoint.Lib.Facades.Declarations;

public interface IAuthFacade
{
    Task<AuthTokenDto> LoginAsync(AuthLoginDto payload, CancellationToken token = default);
}