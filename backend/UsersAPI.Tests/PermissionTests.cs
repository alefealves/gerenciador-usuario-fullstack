using Bogus;
using FluentAssertions;
using System.Net;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Tests.Helpers;
using Xunit;

namespace PermissionApp.API.Tests
{
  public class PermissionTests
  {
    [Fact]
    public async Task Permission_Post_Returns_Created()
    {
      //dados enviados para requisição
      var request = new PermissionAddRequestDto
      {
        RoleId = Guid.Parse("7FBACDF9-FECD-45CB-81AD-FAAC757B6C66"),
        SubModuleId = Guid.Parse("472E0790-E32F-4857-B9C9-960459AA134E"),
        Read = true,
        Create = false,
        Update = false,
        Delete = false
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PostAsync("api/permissions", content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Created);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<PermissionResponseDto>(result);
      response.Id.Should().NotBeEmpty();
      response.RoleId.Should().Be(request.RoleId);
      response.SubModuleId.Should().Be(request.SubModuleId);
      response.Read.Should().Be(request.Read);
      response.Create.Should().Be(request.Create);
      response.Update.Should().Be(request.Update);
      response.Delete.Should().Be(request.Delete);
    }

    [Fact]
    public async Task Permission_Post_Returns_InternalServerError()
    {
      //dados enviados para requisição incompletos
      var request = new PermissionAddRequestDto
      {
        RoleId = Guid.Parse("7FBACDF9-FECD-45CB-81AD-FAAC757B6C66"),
        SubModuleId = Guid.Empty,
        Read = true,
        Create = false,
        Update = false,
        Delete = false
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PostAsync("api/permissions", content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<PermissionResponseDto>(result);
      response.Id.Should().BeEmpty();
      response.RoleId.Should().BeEmpty();
      response.SubModuleId.Should().BeEmpty();
      response.Read.Should().BeFalse();
      response.Create.Should().BeFalse();
      response.Update.Should().BeFalse();
      response.Delete.Should().BeFalse();
    }

    [Fact]
    public async Task Permission_Post_Returns_Unauthorized()
    {
      //dados enviados para requisição
      var request = new PermissionAddRequestDto
      {
        RoleId = Guid.Parse("7FBACDF9-FECD-45CB-81AD-FAAC757B6C66"),
        SubModuleId = Guid.Parse("472E0790-E32F-4857-B9C9-960459AA134E"),
        Read = true,
        Create = false,
        Update = false,
        Delete = false
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.PostAsync("api/permissions", content);

      ///Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Permission_Put_Returns_Ok()
    {
      //dados alterados para nova requisição
      var permissionId = Guid.Parse("DB3B09CC-A4F8-4DE3-96C7-CF7A3BD5F503");
      var request = new PermissionUpdateRequestDto
      {
        Read = true,
        Create = true,
        Update = true,
        Delete = true
      };

      //serializando os dados da requisição
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição PUT para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PutAsync("api/permissions/" + permissionId, content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<PermissionResponseDto>(result);
      response.Id.Should().Be(permissionId);
      response.Read.Should().Be(request.Read);
      response.Create.Should().Be(request.Create);
      response.Update.Should().Be(request.Update);
      response.Delete.Should().Be(request.Delete);
    }

    [Fact]
    public async Task Permission_Put_Returns_Unauthorized()
    {
      //dados alterados para nova requisição
      var permissionId = Guid.Parse("DB3B09CC-A4F8-4DE3-96C7-CF7A3BD5F503");
      var request = new PermissionUpdateRequestDto
      {
        Read = false,
        Create = false,
        Update = false,
        Delete = false
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.PutAsync("api/permissions/" + permissionId, content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Permission_Delete_Returns_Ok()
    {
      var permissionId = Guid.Parse("DB3B09CC-A4F8-4DE3-96C7-CF7A3BD5F503");

      //fazendo a requisição DELETE para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.DeleteAsync("api/permissions/" + permissionId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Permission_Delete_Returns_Unauthorized()
    {
      var permissionId = Guid.Parse("DB3B09CC-A4F8-4DE3-96C7-CF7A3BD5F503");

      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.DeleteAsync("api/permissions/" + permissionId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Permission_Get_Returns_Ok()
    {
      var permissionId = Guid.Parse("DB3B09CC-A4F8-4DE3-96C7-CF7A3BD5F503");

      //fazendo a requisição GET para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.GetAsync("api/permissions/get/" + permissionId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<PermissionResponseDto>(result);
      response.Id.Should().Be(permissionId);
    }

    [Fact]
    public async Task Permission_GetAll_Returns_Ok()
    {
      //fazendo a requisição GET para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.GetAsync("api/permissions/list/");

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<List<PermissionResponseDto>>(result);
      response.Should().NotBeNull();
      response.Count.Should().BeGreaterThan(0);
      response[0].Id.Should().NotBeEmpty();
    }
  }
}