using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;

namespace UsersAPI.Services.Controllers
{
  //[Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class RolesController : ControllerBase
  {
    private readonly IRoleAppService? _roleAppService;

    public RolesController(IRoleAppService? roleAppService)
    {
      _roleAppService = roleAppService;
    }

    /// <summary>
    /// cadastrar um novo perfil
    /// </summary>

    //[AllowAnonymous]
    [Authorize]
    [HttpPost]
    public IActionResult Add([FromBody] RoleAddRequestDto dto)
    {
      return StatusCode(201, _roleAppService?.Add(dto));
    }

    /// <summary>
    /// Alterar os dados da conta do perfil informado
    /// </summary>
    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Edit(Guid id, RoleUpdateRequestDto dto)
    {
      return StatusCode(200, _roleAppService?.Update(id, dto));
    }

    /// <summary>
    /// Excluir o perfil informado
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      return StatusCode(204, _roleAppService?.Delete(id));
    }

    /// <summary>
    /// Lista todos os perfis cadastrados
    /// </summary>
    [HttpGet("list/")]
    [ProducesResponseType(typeof(List<RoleResponseDto>), 200)]
    public IActionResult GetAll()
    {
      return Ok(_roleAppService?.GetAll());
    }

    /// <summary>
    /// Retorna os dados do perfil informado
    /// </summary>
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(List<RoleResponseDto>), 200)]
    public IActionResult Get(Guid id)
    {
      return Ok(_roleAppService?.Get(id));
    }
  }
}
