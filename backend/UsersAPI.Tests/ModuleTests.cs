using Bogus;
using FluentAssertions;
using System.Net;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Tests.Helpers;
using Xunit;

namespace ModuleApp.API.Tests
{
  public class ModuleTests
  {
    [Fact]
    public async Task Module_Post_Returns_Created()
    {
      //dados enviados para requisição
      var request = new ModuleAddRequestDto
      {
        ModuleName = "Relatorios"
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PostAsync("api/modules", content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Created);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<ModuleResponseDto>(result);
      response.Id.Should().NotBeEmpty();
      response.ModuleName.Should().Be(request.ModuleName);
    }

    [Fact]
    public async Task Module_Post_Returns_BadRequest()
    {
      //dados enviados para requisição com ModuleName vazio
      var request = new ModuleAddRequestDto
      {
        ModuleName = string.Empty
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição POST para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PostAsync("api/modules", content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<ModuleResponseDto>(result);
      response.Id.Should().BeEmpty();
      response.ModuleName.Should().BeNull();
    }

    [Fact]
    public async Task Module_Post_Returns_Unauthorized()
    {
      //dados enviados para requisição
      var request = new ModuleAddRequestDto
      {
        ModuleName = "Relatorios"
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.PostAsync("api/modules", content);

      ///Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }


    [Fact]
    public async Task Module_Put_Returns_Ok()
    {
      var ModuleId = Guid.Parse("4E1D674E-7082-42B2-866E-0696737BB113");
      var ModuleName = "Relatorios";

      //dados alterados para nova requisição
      var request = new ModuleUpdateRequestDto
      {
        ModuleName = ModuleName + " Alterado"
      };

      //serializando os dados da requisição
      var content = TestHelper.CreateContent(request);

      //fazendo a requisição PUT para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.PutAsync("api/modules/" + ModuleId, content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<ModuleResponseDto>(result);
      response.Id.Should().Be(ModuleId);
      response.ModuleName.Should().Be(response.ModuleName);
    }

    [Fact]
    public async Task Module_Put_Returns_Unauthorized()
    {
      var ModuleId = Guid.Parse("4E1D674E-7082-42B2-866E-0696737BB113");
      var ModuleName = "Interactions";

      //dados alterados para nova requisição
      var request = new ModuleUpdateRequestDto
      {
        ModuleName = ModuleName + " Alterado"
      };

      //serializando os dados da requisição   
      var content = TestHelper.CreateContent(request);

      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.PutAsync("api/modules/" + ModuleId, content);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Module_Delete_Returns_Ok()
    {
      var ModuleId = Guid.Parse("4E1D674E-7082-42B2-866E-0696737BB113");

      //fazendo a requisição DELETE para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.DeleteAsync("api/modules/" + ModuleId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Module_Delete_Returns_Unauthorized()
    {
      var ModuleId = Guid.Parse("4E1D674E-7082-42B2-866E-0696737BB113");
      //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
      var client = TestHelper.CreateUnauthorizedClient();
      var result = await client.DeleteAsync("api/modules/" + ModuleId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Module_Get_Returns_Ok()
    {
      var ModuleId = Guid.Parse("4E1D674E-7082-42B2-866E-0696737BB113");

      //fazendo a requisição GET para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.GetAsync("api/modules/get/" + ModuleId);

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<ModuleResponseDto>(result);
      response.Id.Should().NotBeEmpty();
      response.ModuleName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Module_GetAll_Returns_Ok()
    {
      //fazendo a requisição GET para API com autenticação
      var client = await TestHelper.CreateAuthorizedClient();
      var result = await client.GetAsync("api/modules/list/");

      //Verificando o status code de resposta
      result.StatusCode.Should().Be(HttpStatusCode.OK);

      //Capturando e verificando o conteúdo da resposta
      var response = TestHelper.ReadResponse<List<ModuleResponseDto>>(result);
      response.Should().NotBeNull();
      response.Count.Should().BeGreaterThan(0);
      response[0].Id.Should().NotBeEmpty();
      response[0].ModuleName.Should().NotBeNullOrEmpty();
    }

  }

}