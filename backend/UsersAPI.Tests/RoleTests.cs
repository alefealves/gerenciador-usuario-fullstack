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

            //fazendo a requisição POST para API
            var result = await TestHelper.CreateClient.PostAsync("api/roles", content);

            //Capturando e verificando o status de resposta
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            //Capturando e verificando o conteúdo da resposta
            var response = TestHelper.ReadResponse<RoleResponseDto>(result);
            response.Id.Should().NotBeEmpty();
            response.RoleName.Should().Be(request.RoleName);

        }

        [Fact(Skip = "Não implementado.")]
        public void Role_Post_Returns_BadRequest()
        {
            //TODO
        }
        /*
                [Fact(Skip = "Não implementado.")]
                public void Role_Put_Returns_Ok()
                {
                    //TODO
                }
                [Fact(Skip = "Não implementado.")]
                public void Role_Put_Returns_Unathorized()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void Role_Delete_Returns_Ok()
                {
                    //TODO
                }
                [Fact(Skip = "Não implementado.")]
                public void Role_Delete_Returns_Unathorized()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void Role_Get_Returns_Ok()
                {
                    //TODO
                }


                [Fact(Skip = "Não implementado.")]
                public void Module_Post_Returns_Ok()
                {
                    //TODO
                }
                [Fact(Skip = "Não implementado.")]
                public void Module_Post_Returns_BadRequest()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void Module_Put_Returns_Ok()
                {
                    //TODO
                }
                [Fact(Skip = "Não implementado.")]
                public void Module_Put_Returns_Unathorized()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void Module_Delete_Returns_Ok()
                {
                    //TODO
                }
                [Fact(Skip = "Não implementado.")]
                public void Module_Delete_Returns_Unathorized()
                {
                    //TODO
                }

                [Fact(Skip = "Não implementado.")]
                public void Module_Get_Returns_Ok()
                {
                    //TODO
                }


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
