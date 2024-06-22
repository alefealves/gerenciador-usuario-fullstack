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
      var result = await TestHelper.CreateClient.PostAsync("api/users", content);

      result.StatusCode.Should().Be(HttpStatusCode.Created);

      var response = TestHelper.ReadResponse<UserResponseDto>(result);
      response.Id.Should().NotBeEmpty();
      response.FirstName.Should().Be(request.FirstName);
      response.LastName.Should().Be(request.LastName);
      response.Email.Should().Be(request.Email);
      response.Active.Should().BeTrue();
      response.EmailConfirmed.Should().BeFalse();
    }

    [Fact(Skip = "Não implementado.")]
    public void Users_Post_Returns_BadRequest()
    {
      //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Users_Put_Returns_Ok()
    {
      //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Users_Delete_Returns_Ok()
    {
      //TODO
    }

    [Fact(Skip = "Não implementado.")]
    public void Users_Get_Returns_Ok()
    {
      //TODO
    }
  }
}
