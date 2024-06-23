using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using System.Net;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Tests.Helpers;
using Xunit;
namespace UsersAPI.Tests
{
  public class AuthTests
  {
    [Fact]
    public async Task Auth_Post_Login_Returns_Ok()
    {
      // Dados de login de exemplo
      var request = new LoginRequestDto
      {
        Email = "admin@example.com",
        Password = "@Admin123"
      };

      var content = TestHelper.CreateContent(request);

      // Fazendo a requisição de login para obter o token JWT
      var client = TestHelper.CreateClient;
      var loginResponse = await client.PostAsync("api/auth/login", content);

      //Verificando o status code de resposta
      loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var loginResult = TestHelper.ReadResponse<LoginResponseDto>(loginResponse);
      loginResult.AccessToken.Should().NotBeNullOrEmpty();
      loginResult.Expiration.Should().NotBeNull();
      loginResult.User.Should().NotBeNull();

      var token = loginResult.AccessToken;

      // Configurando o token JWT no cabeçalho de autorização
      client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    [Fact]
    public async Task Auth_Post_Login_Returns_BadRequest()
    {
      // Dados de login de exemplo
      var request = new LoginRequestDto
      {
        Email = "admin@example.com",
        Password = "senhaerrada"
      };

      var content = TestHelper.CreateContent(request);

      // Fazendo a requisição de login para obter o token JWT
      var client = TestHelper.CreateClient;
      var loginResponse = await client.PostAsync("api/auth/login", content);

      //Verificando o status code de resposta
      loginResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

      //Capturando e verificando o conteúdo da resposta
      var loginResult = TestHelper.ReadResponse<LoginResponseDto>(loginResponse);
      loginResult.AccessToken.Should().BeNullOrEmpty();
      loginResult.Expiration.Should().BeNull();
      loginResult.User.Should().BeNull();
    }

    [Fact]
    public async Task Auth_Post_Activate_User_Returns_Ok()
    {
      var userId = Guid.Parse("5F631F4D-8E0E-4A90-8312-83FA496E2A8C");
      // Dados de login de exemplo
      var request = new ActivateUserRequestDto
      {
        Id = userId,
      };

      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var response = await client.PostAsync("api/auth/activate-user/" + userId, content);

      //Verificando o status code de resposta
      response.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var result = TestHelper.ReadResponse<UserResponseDto>(response);
      result.Id.Should().Be(userId);
      //TODO: Verificar se o usuário foi ativado
      result.Active.Should().BeTrue();
    }

    [Fact]
    public async Task Auth_Post_Activate_User_Returns_NotFound()
    {
      // Passando userId vazio
      var userId = Guid.Empty;
      var request = new ActivateUserRequestDto
      {
        Id = userId,
      };

      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      //fazendo a requisição sem passar o userId por parâmetro
      var response = await client.PostAsync("api/auth/activate-user/", content);

      //Verificando o status code de resposta
      response.StatusCode.Should().Be(HttpStatusCode.NotFound);

      //Capturando e verificando o conteúdo da resposta
      var result = TestHelper.ReadResponse<UserResponseDto>(response);
      result.Id.Should().BeEmpty();
    }

    [Fact]
    public async Task Auth_Post_Activate_User_Returns_InternalServerError()
    {
      // Passando userId vazio
      var userId = Guid.Empty;
      var request = new ActivateUserRequestDto
      {
        Id = userId,
      };

      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      //fazendo a requisição passando o userId vazio
      var response = await client.PostAsync("api/auth/activate-user/" + userId, content);

      //Verificando o status code de resposta
      response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

      //Capturando e verificando o conteúdo da resposta
      var result = TestHelper.ReadResponse<UserResponseDto>(response);
      result.Id.Should().BeEmpty();
    }

    [Fact]
    public async Task Auth_Post_Activate_User_Returns_Unauthorized()
    {
      // userId de exemplo
      var userId = Guid.Parse("5F631F4D-8E0E-4A90-8312-83FA496E2A8C");
      var request = new ActivateUserRequestDto
      {
        Id = userId,
      };

      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = TestHelper.CreateUnauthorizedClient();
      var response = await client.PostAsync("api/auth/activate-user/" + userId, content);

      //Verificando o status code de resposta
      response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void Auth_Post_Forgot_Password_Returns_Ok()
    {
      //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Auth_Post_Forgot_Password_Returns_BadRequest()
    {
      //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Auth_Post_Reset_Password_Returns_Ok()
    {
      //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Auth_Post_Reset_Password_Returns_BadRequest()
    {
      //TODO
    }
  }
}
