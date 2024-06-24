using Bogus;
using FluentAssertions;
using System.Net;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Tests.Helpers;
using Xunit;

namespace SubModuleApp.API.Tests
{
  public class SubModuleTests
  {
    [Fact]
    public async Task SubModule_Post_Returns_Ok()
    {
      //dados enviados para requisição
      var request = new SubModuleAddRequestDto
      {
        ModuleId = Guid.Parse("D1EF9B74-05FC-446C-AD6C-BB3D3AB65D37"),
        ParentSubModuleId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
        SubModuleName = "Relatorios"
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PostAsync("api/submodules", content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Created);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<SubModuleResponseDto>(result);
      response.Id.Should().NotBeEmpty();
      response.ModuleId.Should().Be(request.ModuleId);
      response.ParentSubModuleId.Should().Be(request.ParentSubModuleId);
      response.SubModuleName.Should().Be(request.SubModuleName);
    }

    [Fact]
    public async Task SubModule_Post_Returns_BadRequest()
    {
      //dados enviados para requisição
      var request = new SubModuleAddRequestDto
      {
        ModuleId = Guid.Empty,
        ParentSubModuleId = Guid.Empty,
        SubModuleName = "Relatorios"
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PostAsync("api/submodules", content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<SubModuleResponseDto>(result);
      response.Id.Should().BeEmpty();
      response.ModuleId.Should().BeEmpty();
      response.ParentSubModuleId.Should().BeEmpty();
      response.SubModuleName.Should().BeNull();
    }

    [Fact]
    public async Task SubModule_Post_Returns_Unauthorized()
    {
      //dados enviados para requisição
      var request = new SubModuleAddRequestDto
      {
        ModuleId = Guid.Parse("D1EF9B74-05FC-446C-AD6C-BB3D3AB65D37"),
        ParentSubModuleId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
        SubModuleName = "Relatorios"
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.PostAsync("api/submodules", content);

      ///Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task SubModule_Put_Returns_Ok()
    {
      var SubModuleId = Guid.Parse("352E7C09-7B78-41F2-9FD0-9BCC0FD5ED2C");
      var SubModuleName = "Relatorios";

      //dados alterados para nova requisição
      var request = new SubModuleUpdateRequestDto
      {
        SubModuleName = SubModuleName + " Alterado"
      };

      //serializando os dados da requisição
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição PUT para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PutAsync("api/submodules/" + SubModuleId, content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<SubModuleResponseDto>(result);
      response.Id.Should().Be(SubModuleId);
      response.SubModuleName.Should().Be(request.SubModuleName);
    }

    [Fact]
    public async Task SubModule_Put_Returns_Unathorized()
    {
      var SubModuleId = Guid.Parse("352E7C09-7B78-41F2-9FD0-9BCC0FD5ED2C");
      var SubModuleName = "Relatorios";

      //dados alterados para nova requisição
      var request = new SubModuleUpdateRequestDto
      {
        SubModuleName = SubModuleName + " Alterado"
      };

      //serializando os dados da requisição
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição PUT para API com autenticação
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.PutAsync("api/submodules/" + SubModuleId, content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task SubModule_Delete_Returns_Ok()
    {
      var SubModuleId = Guid.Parse("472E0790-E32F-4857-B9C9-960459AA134E");

      //fazendo a requisição DELETE para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.DeleteAsync("api/submodules/" + SubModuleId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task SubModule_Delete_Returns_Unathorized()
    {
      var SubModuleId = Guid.Parse("352E7C09-7B78-41F2-9FD0-9BCC0FD5ED2C");

      //fazendo a requisição DELETE para API com autenticação
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.DeleteAsync("api/submodules/" + SubModuleId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task SubModule_Get_Returns_Ok()
    {
      var SubModuleId = Guid.Parse("352E7C09-7B78-41F2-9FD0-9BCC0FD5ED2C");

      //fazendo a requisição GET para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.GetAsync("api/submodules/get/" + SubModuleId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<SubModuleResponseDto>(result);
      response.Id.Should().NotBeEmpty();
      response.ModuleId.Should().NotBeEmpty();
      //response.ParentSubModuleId.Should().NotBeEmpty();
      response.SubModuleName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task SubModule_GetAll_Returns_Ok()
    {
      //fazendo a requisição GET para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.GetAsync("api/submodules/list/");

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<List<SubModuleResponseDto>>(result);
      response.Should().NotBeNull();
      response.Count.Should().BeGreaterThan(0);
      response[0].Id.Should().NotBeEmpty();
      response[0].ModuleId.Should().NotBeEmpty();
      //response[0].ParentSubModuleId.Should().NotBeEmpty();
      response[0].SubModuleName.Should().NotBeNullOrEmpty();
    }

    [Fact(Skip = "Não implementado.")]
    public void Module_Associate_SubModule_Post_Returns_Ok()
    {
      //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Module_Associate_SubModule_Post_Returns_Unathorized()
    {
      //TODO
    }

  }
}

