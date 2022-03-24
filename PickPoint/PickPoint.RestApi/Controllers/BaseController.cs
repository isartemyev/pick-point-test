using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Domain.Exceptions;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.RestApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiConventionType(typeof(DefaultApiConventions))]
public class BaseController : Controller
{
    private readonly IMerchantRepository _repository;

    public BaseController(IMerchantRepository repository)
    {
        _repository = repository;
    }
        
    protected IActionResult Created(object value = null)
    {
        return StatusCode(StatusCodes.Status201Created, value);
    }

    protected IActionResult Forbidden(object value = null)
    {
        return StatusCode(StatusCodes.Status403Forbidden, value);
    }

    protected IActionResult InternalError(object value = null)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, value);
    }

    protected string GetUserId() => User.Claims.SingleOrDefault(x => x.Type == "merchantId")?.Value;

    protected async Task<PickPointMerchantEntity> GetCurrentUserAsync()
    {
        var id = GetUserId();

        if (id is null)
        {
            return new PickPointMerchantEntity().SetRole(EMerchantRole.Anonymous);
        }

        var user = await _repository.ReadAsync(id);

        if (user is null)
        {
            throw new PickPointUnauthorizedAccessException();
        }

        return user;
    }
}