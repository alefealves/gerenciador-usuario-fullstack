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
  public class SubModulesController : ControllerBase
  {
    private readonly ISubModuleAppService? _subModuleAppService;

    public SubModulesController(ISubModuleAppService? SubModuleAppService)
    {
      _subModuleAppService = SubModuleAppService;
    }

    /// <summary>
    /// cadastrar um novo submódulo
    /// </summary>
    //[AllowAnonymous]
    [HttpPost]
    public IActionResult Add([FromBody] SubModuleAddRequestDto dto)
    {
      return StatusCode(201, _subModuleAppService?.Add(dto));
    }

    /// <summary>
    /// Alterar os dados do submódulo informado
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult Edit(Guid id, SubModuleUpdateRequestDto dto)
    {
      return StatusCode(200, _subModuleAppService?.Update(id, dto));
    }

    /// <summary>
    /// Excluir o submódulo informado
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      return StatusCode(204, _subModuleAppService?.Delete(id));
    }

    /// <summary>
    /// Lista todos os submódulos cadastrados
    /// </summary>
    [HttpGet("list/")]
    [ProducesResponseType(typeof(List<SubModuleResponseDto>), 200)]
    public IActionResult GetAll()
    {
      return Ok(_subModuleAppService?.GetAll());
    }

    /// <summary>
    /// Lista todos os submódulos cadastrados da módulo informado
    /// </summary>
    [HttpGet("List/{moduleId}")]
    [ProducesResponseType(typeof(List<UserResponseDto>), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAll(Guid moduleId)
    {
      return StatusCode(200, _subModuleAppService?.GetAll(moduleId));
    }

    /// <summary>
    /// Retorna os dados do submódulo informado
    /// </summary>
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(List<SubModuleResponseDto>), 200)]
    public IActionResult Get(Guid id)
    {
      return Ok(_subModuleAppService?.Get(id));
    }

  }
}
