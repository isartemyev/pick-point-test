using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PickPoint.Lib.Dto.Merchant;
using PickPoint.Lib.Facades.Declarations;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.RestApi.Controllers;

public class MerchantController : BaseController
{
    private readonly IMerchantFacade _facade;

    public MerchantController(IMerchantRepository merchantRepository, IMerchantFacade facade) : base(merchantRepository)
    {
        _facade = facade ?? throw new ArgumentNullException(nameof(facade));
    }
        
    [HttpPost]
    [Description("Создать пользователя")]
    public async Task<IActionResult> Create([FromBody] MerchantCreateDto dto)
    {
        var user   = await GetCurrentUserAsync();
        var entity = await _facade.CreateAsync(dto, user);

        return Created(entity);
    }

    [HttpGet]
    [Description("Получить пользователя по id")]
    public async Task<IActionResult> Read([FromQuery, Required] string id)
    {
        var user   = await GetCurrentUserAsync();
        var result = await _facade.ReadAsync(id, user);

        return Ok(result);
    }

    [HttpPut]
    [Description("Обновить информацию о пользователе")]
    public async Task<IActionResult> Update([FromBody] MerchantUpdateDto dto)
    {
        var user = await GetCurrentUserAsync();
        await _facade.UpdateAsync(dto, user);

        return Ok();
    }

    [HttpDelete]
    [Description("Удалить пользователя по id")]
    public async Task<IActionResult> Delete([FromBody, Required] string id)
    {
        var user   = await GetCurrentUserAsync();
        var result = await _facade.DeleteAsync(id, user);

        return result ? Ok() : InternalError();
    }

    [HttpGet]
    [Description("Получить список пользователей")]
    public async Task<IActionResult> List([FromQuery] MerchantFilterDto filter)
    {
        var user = await GetCurrentUserAsync();
        var list = await _facade.ListAsync(filter, user);

        if (!list.Any())
        {
            return NoContent();
        }

        return Ok(list);
    }
}