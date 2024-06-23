using Bogus;
using FluentAssertions;
using System.Net;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Tests.Helpers;
using Xunit;

namespace UsersAPI.Tests
{
  public class UsersTests
  {
    [Fact]
    public async Task Users_Post_Returns_Created()
    {
      var faker = new Faker("pt_BR");
      var request = new UserAddRequestDto
      {
        RoleId = Guid.Parse("7FBACDF9-FECD-45CB-81AD-FAAC757B6C66"),
        FirstName = faker.Person.FirstName,
        LastName = faker.Person.LastName,
        Email = faker.Person.Email,
        Password = "@Teste123",
        PasswordConfirm = "@Teste123"
      };

      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PostAsync("api/users", content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Created);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<UserResponseDto>(result);
      response.Id.Should().NotBeEmpty();
      response.FirstName.Should().Be(request.FirstName);
      response.LastName.Should().Be(request.LastName);
      response.Email.Should().Be(request.Email);
      response.Active.Should().BeTrue();
      response.EmailConfirmed.Should().BeFalse();
    }

    [Fact]
    public async Task Users_Post_Returns_BadRequest()
    {
      //dados enviados para requisição com campo required RoleId vazio
      var faker = new Faker("pt_BR");
      var request = new UserAddRequestDto
      {
        RoleId = Guid.Empty,
        FirstName = faker.Person.FirstName,
        LastName = faker.Person.LastName,
        Email = faker.Person.Email,
        Password = "@Teste123",
        PasswordConfirm = "@Teste123"
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PostAsync("api/users", content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<RoleResponseDto>(result);
      response.Id.Should().BeEmpty();
      response.RoleName.Should().BeNull();
    }

    [Fact]
    public async Task Users_Post_Returns_Unauthorized()
    {
      //dados enviados para requisição
      var faker = new Faker("pt_BR");
      var request = new UserAddRequestDto
      {
        RoleId = Guid.Parse("7FBACDF9-FECD-45CB-81AD-FAAC757B6C66"),
        FirstName = faker.Person.FirstName,
        LastName = faker.Person.LastName,
        Email = faker.Person.Email,
        Password = "@Teste123",
        PasswordConfirm = "@Teste123"
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.PostAsync("api/users", content);

      ///Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }


    [Fact]
    public async Task Users_Put_Returns_Ok()
    {
      var userId = Guid.Parse("5F631F4D-8E0E-4A90-8312-83FA496E2A8C");

      //dados alterados para nova requisição
      var faker = new Faker("pt_BR");
      var request = new UserUpdateRequestDto
      {
        RoleId = Guid.Parse("7FBACDF9-FECD-45CB-81AD-FAAC757B6C66"),
        FirstName = faker.Person.FirstName + " Alterado",
        LastName = faker.Person.LastName + " Alterado",
        EmailConfirmed = false,
        Active = false
      };

      //serializando os dados da requisição
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição PUT para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PutAsync("api/users/" + userId, content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<UserResponseDto>(result);
      response.Id.Should().Be(userId);
      response.FirstName.Should().Be(request.FirstName);
      response.LastName.Should().Be(request.LastName);
      response.EmailConfirmed.Should().Be(request.EmailConfirmed);
      response.Active.Should().Be(request.Active);
      response.RoleId.Should().Be(request.RoleId);
    }

    [Fact]
    public async Task Users_Put_Returns_Unauthorized()
    {
      var userId = Guid.Parse("5F631F4D-8E0E-4A90-8312-83FA496E2A8C");

      //dados alterados para nova requisição
      var faker = new Faker("pt_BR");
      var request = new UserUpdateRequestDto
      {
        RoleId = Guid.Parse("7FBACDF9-FECD-45CB-81AD-FAAC757B6C66"),
        FirstName = faker.Person.FirstName + " Alterado",
        LastName = faker.Person.LastName + " Alterado",
        EmailConfirmed = false,
        Active = false
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.PutAsync("api/users/" + userId, content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Users_Delete_Returns_Ok()
    {
      var userId = Guid.Parse("9332FDE1-60E7-4529-B63B-3E9550385B60");

      //fazendo a requisição DELETE para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.DeleteAsync("api/users/" + userId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Users_Delete_Returns_Unauthorized()
    {
      var userId = Guid.Parse("9332FDE1-60E7-4529-B63B-3E9550385B60");

      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.DeleteAsync("api/users/" + userId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Users_Get_Returns_Ok()
    {
      var userId = Guid.Parse("9332FDE1-60E7-4529-B63B-3E9550385B60");

      //fazendo a requisição GET para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.GetAsync("api/users/get/" + userId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<UserResponseDto>(result);
      response.Id.Should().NotBeEmpty();
      response.Email.Should().NotBeNullOrEmpty();
      response.RoleName.Should().NotBeNullOrEmpty();
      response.FirstName.Should().NotBeNullOrEmpty();
      response.LastName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Users_GetAll_Returns_Ok()
    {
      //fazendo a requisição GET para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.GetAsync("api/users/list/");

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<List<UserResponseDto>>(result);
      response.Should().NotBeNull();
      response.Count.Should().BeGreaterThan(0);
      response[0].Id.Should().NotBeEmpty();
      response[0].RoleName.Should().NotBeNullOrEmpty();
      response[0].FirstName.Should().NotBeNullOrEmpty();
      response[0].LastName.Should().NotBeNullOrEmpty();
      response[0].Email.Should().NotBeNullOrEmpty();
    }
  }
}
