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
  public class UsersController : ControllerBase
  {
    private readonly IUserAppService? _userAppService;

    public UsersController(IUserAppService? userAppService)
    {
      _userAppService = userAppService;
    }

    /// <summary>
    /// Cadastrar a conta de um novo usuário
    /// </summary>
    //[AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(UserResponseDto), 201)]
    public IActionResult Add([FromBody] UserAddRequestDto dto)
    {
      return StatusCode(201, _userAppService?.Add(dto));
    }

    /// <summary>
    /// Alterar os dados do usuário informado
    /// </summary>
    [HttpPut("{id}")]
    public IActionResult Edit(Guid id, UserUpdateRequestDto dto)
    {
      return StatusCode(200, _userAppService?.Update(id, dto));
    }

    /// <summary>
    /// Excluir o usuário informado
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      return StatusCode(204, _userAppService?.Delete(id));
    }

    /// <summary>
    /// Lista todos os usuários cadastrados
    /// </summary>
    [HttpGet("List/")]
    [ProducesResponseType(typeof(List<UserResponseDto>), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAll()
    {
      return StatusCode(200, _userAppService?.GetAll());
    }

    /// <summary>
    /// Retorna os dados do usuário informado
    /// </summary>
    [HttpGet("Get/{id}")]
    [ProducesResponseType(typeof(UserResponseDto), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(Guid id)
    {
      return StatusCode(200, _userAppService?.Get(id));
    }
  }
}
