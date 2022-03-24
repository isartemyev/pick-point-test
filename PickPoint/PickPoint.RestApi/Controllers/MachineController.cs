using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PickPoint.Lib.Dto.Machine;
using PickPoint.Lib.Facades.Declarations;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.RestApi.Controllers;

public class MachineController : BaseController
{
    private readonly IMachineFacade _facade;

    public MachineController(IMerchantRepository merchantRepository, IMachineFacade facade) : base(merchantRepository)
    {
        _facade = facade ?? throw new ArgumentNullException(nameof(facade));
    }
        
    [HttpPost]
    [Description("Создать постомат")]
    public async Task<IActionResult> Create([FromBody] MachineCreateDto dto)
    {
        var user   = await GetCurrentUserAsync();
        var entity = await _facade.CreateAsync(dto, user);

        return Created(entity);
    }

    [HttpGet]
    [Description("Получить постомат по id")]
    public async Task<IActionResult> Read([FromQuery, Required] string id)
    {
        var user   = await GetCurrentUserAsync();
        var result = await _facade.ReadAsync(id, user);

        return Ok(result);
    }

    [HttpPut]
    [Description("Обновить информацию о постомате")]
    public async Task<IActionResult> Update([FromBody] MachineUpdateDto dto)
    {
        var user = await GetCurrentUserAsync();
        await _facade.UpdateAsync(dto, user);

        return Ok();
    }

    [HttpDelete]
    [Description("Удалить постомат по id")]
    public async Task<IActionResult> Delete([FromBody, Required] string id)
    {
        var user   = await GetCurrentUserAsync();
        var result = await _facade.DeleteAsync(id, user);

        return result ? Ok() : InternalError();
    }

    [HttpGet]
    [Description("Получить список постоматов")]
    public async Task<IActionResult> List([FromQuery] MachineFilterDto filter)
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