using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;

namespace UsersAPI.Services.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class PermissionsController : ControllerBase
  {
    private readonly IPermissionAppService? _permissionAppService;

    public PermissionsController(IPermissionAppService? permissionAppService)
    {
      _permissionAppService = permissionAppService;
    }

    /// <summary>
    /// cadastrar uma nova permissão
    /// </summary>

    //[AllowAnonymous]
    [HttpPost]
    public IActionResult Add([FromBody] PermissionAddRequestDto dto)
    {
      return StatusCode(201, _permissionAppService?.Add(dto));
    }

    /// <summary>
    /// Alterar os dados da permissão informada
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult Edit(Guid id, PermissionUpdateRequestDto dto)
    {
      return StatusCode(200, _permissionAppService?.Update(id, dto));
    }

    /// <summary>
    /// Excluir a permissão informada
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      return StatusCode(204, _permissionAppService?.Delete(id));
    }

    /// <summary>
    /// Lista todos as permissões cadastradas
    /// </summary>
    [HttpGet("list/")]
    [ProducesResponseType(typeof(List<PermissionResponseDto>), 200)]
    public IActionResult GetAll()
    {
      return Ok(_permissionAppService?.GetAll());
    }

    /// <summary>
    /// Retorna os dados da permissão informada
    /// </summary>
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(List<PermissionResponseDto>), 200)]
    public IActionResult Get(Guid id)
    {
      return Ok(_permissionAppService?.Get(id));

    }
  }
}
