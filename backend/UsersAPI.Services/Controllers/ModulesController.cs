using Microsoft.AspNetCore.Mvc;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;

namespace UsersAPI.Services.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ModulesController : ControllerBase
  {
    private readonly IModuleAppService? _moduleAppService;

    public ModulesController(IModuleAppService? ModuleAppService)
    {
      _moduleAppService = ModuleAppService;
    }

    /// <summary>
    /// cadastrar um novo módulo
    /// </summary>
    //[AllowAnonymous]
    [HttpPost]
    public IActionResult Add([FromBody] ModuleAddRequestDto dto)
    {
      return StatusCode(201, _moduleAppService?.Add(dto));
    }

    /// <summary>
    /// Alterar os dados do módulo informado
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult Edit(Guid id, ModuleUpdateRequestDto dto)
    {
      return StatusCode(200, _moduleAppService?.Update(id, dto));
    }

    /// <summary>
    /// Excluir o módulo informado
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      return StatusCode(200, _moduleAppService?.Delete(id));
    }

    /// <summary>
    /// Lista todos os módulos cadastrados
    /// </summary>
    [HttpGet("list/")]
    [ProducesResponseType(typeof(List<ModuleResponseDto>), 200)]
    public IActionResult GetAll()
    {
      return Ok(_moduleAppService?.GetAll());
    }

    /// <summary>
    /// Retorna os dados do módulo informado
    /// </summary>
    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(List<ModuleResponseDto>), 200)]
    public IActionResult Get(Guid id)
    {
      return Ok(_moduleAppService?.Get(id));
    }
  }
}
