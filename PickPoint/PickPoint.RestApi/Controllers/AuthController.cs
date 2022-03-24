using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PickPoint.Lib.Dto.Auth;
using PickPoint.Lib.Facades.Declarations;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.RestApi.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IAuthFacade _facade;

    public AuthController(IMerchantRepository merchantRepository, IAuthFacade accountFacade) : base(merchantRepository)
    {
        _facade = accountFacade ?? throw new ArgumentNullException(nameof(accountFacade));
    }
        
    [HttpPost]
    [Description("Выполнить авторизацию")]
    public async Task<IActionResult> Login([FromBody] AuthLoginDto dto)
    {
        var token = await _facade.LoginAsync(dto);

        return Ok(token);
    }
}