using Bogus;
using FluentAssertions;
using System.Net;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Tests.Helpers;
using Xunit;

namespace RoleApp.API.Tests
{
    public class RoleTests
    {
        [Fact]
        public async Task Role_Post_Returns_Created()
        {
            //dados enviados para requisição
            var faker = new Faker("pt_BR");
            var request = new RoleAddRequestDto
            {
                RoleName = faker.Name.JobArea()
            };

            //serializando os dados da requisição   
            var content = TestHelper.CreateContent(request);

            //fazendo a requisição POST para API com autenticação
            var client = await TestHelper.CreateAuthorizedClient();
            var result = await client.PostAsync("api/roles", content);

            //Verificando o status code de resposta
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            //Capturando e verificando o conteúdo da resposta
            var response = TestHelper.ReadResponse<RoleResponseDto>(result);
            response.Id.Should().NotBeEmpty();
            response.RoleName.Should().Be(request.RoleName);
        }

        [Fact]
        public async Task Role_Post_Returns_BadRequest()
        {
            //dados enviados para requisição com RoleName vazio
            var request = new RoleAddRequestDto
            {
                RoleName = string.Empty
            };

            //serializando os dados da requisição   
            var content = TestHelper.CreateContent(request);

            //fazendo a requisição POST para API com autenticação
            var client = await TestHelper.CreateAuthorizedClient();
            var result = await client.PostAsync("api/roles", content);

            //Verificando o status code de resposta
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //Capturando e verificando o conteúdo da resposta
            var response = TestHelper.ReadResponse<RoleResponseDto>(result);
            response.Id.Should().BeEmpty();
            response.RoleName.Should().BeNull();
        }

        [Fact]
        public async Task Role_Post_Returns_Unauthorized()
        {
            //dados enviados para requisição
            var faker = new Faker("pt_BR");
            var request = new RoleAddRequestDto
            {
                RoleName = faker.Name.JobArea()
            };

            //serializando os dados da requisição   
            var content = TestHelper.CreateContent(request);

            //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
            var client = TestHelper.CreateUnauthorizedClient();
            var result = await client.PostAsync("api/roles", content);

            ///Verificando o status code de resposta
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


        [Fact]
        public async Task Role_Put_Returns_Ok()
        {
            var roleId = Guid.Parse("D434208A-3E05-4705-8593-69491ABC2B9A");
            var roleName = "Interactions";

            //dados alterados para nova requisição
            var request = new RoleUpdateRequestDto
            {
                RoleName = roleName + " Alterado"
            };

            //serializando os dados da requisição
            var content = TestHelper.CreateContent(request);

            //fazendo a requisição PUT para API com autenticação
            var client = await TestHelper.CreateAuthorizedClient();
            var result = await client.PutAsync("api/roles/" + roleId, content);

            //Verificando o status code de resposta
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //Capturando e verificando o conteúdo da resposta
            var response = TestHelper.ReadResponse<RoleResponseDto>(result);
            response.Id.Should().Be(roleId);
            response.RoleName.Should().Be(response.RoleName);
        }

        [Fact]
        public async Task Role_Put_Returns_Unauthorized()
        {
            var roleId = Guid.Parse("D434208A-3E05-4705-8593-69491ABC2B9A");
            var roleName = "Interactions";

            //dados alterados para nova requisição
            var request = new RoleUpdateRequestDto
            {
                RoleName = roleName + " Alterado"
            };

            //serializando os dados da requisição   
            var content = TestHelper.CreateContent(request);

            //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
            var client = TestHelper.CreateUnauthorizedClient();
            var result = await client.PutAsync("api/roles/" + roleId, content);

            //Verificando o status code de resposta
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Role_Delete_Returns_Ok()
        {
            var roleId = Guid.Parse("D434208A-3E05-4705-8593-69491ABC2B9A");

            //fazendo a requisição DELETE para API com autenticação
            var client = await TestHelper.CreateAuthorizedClient();
            var result = await client.DeleteAsync("api/roles/" + roleId);

            //Verificando o status code de resposta
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Role_Delete_Returns_Unauthorized()
        {
            var roleId = Guid.Parse("5072F67A-FD1A-4BC6-8719-BA2C6480A5C4");
            //Simular usuário não autorizado removendo as credenciais ou usando um cliente não autenticado
            var client = TestHelper.CreateUnauthorizedClient();
            var result = await client.DeleteAsync("api/roles/" + roleId);

            //Verificando o status code de resposta
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Role_Get_Returns_Ok()
        {
            var roleId = Guid.Parse("9863F9CE-1B42-46E6-8E25-EC91D8117B23");

            //fazendo a requisição GET para API com autenticação
            var client = await TestHelper.CreateAuthorizedClient();
            var result = await client.GetAsync("api/roles/get/" + roleId);

            //Verificando o status code de resposta
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //Capturando e verificando o conteúdo da resposta
            var response = TestHelper.ReadResponse<RoleResponseDto>(result);
            response.Id.Should().NotBeEmpty();
            response.RoleName.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Role_GetAll_Returns_Ok()
        {
            //fazendo a requisição GET para API com autenticação
            var client = await TestHelper.CreateAuthorizedClient();
            var result = await client.GetAsync("api/roles/list/");

            //Verificando o status code de resposta
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //Capturando e verificando o conteúdo da resposta
            var response = TestHelper.ReadResponse<List<RoleResponseDto>>(result);
            response.Should().NotBeNull();
            response.Count.Should().BeGreaterThan(0);
            response[0].Id.Should().NotBeEmpty();
            response[0].RoleName.Should().NotBeNullOrEmpty();
        }

        /*
                

                [Fact(Skip = "Não implementado.")]
                public void ChildModule_Post_Returns_Ok()
                {
                    //TODO
                }
                [Fact(Skip = "Não implementado.")]
                public void ChildModule_Post_Returns_BadRequest()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void ChildModule_Put_Returns_Ok()
                {
                    //TODO
                }
                [Fact(Skip = "Não implementado.")]
                public void ChildModule_Put_Returns_Unathorized()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void ChildModule_Delete_Returns_Ok()
                {
                    //TODO
                }
                [Fact(Skip = "Não implementado.")]
                public void ChildModule_Delete_Returns_Unathorized()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void ChildModule_Get_Returns_Ok()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void Module_Associate_ChildModule_Post_Returns_Ok()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void Module_Associate_ChildModule_Post_Returns_Unathorized()
                {
                    //TODO
                }
        */
    }
}
