using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;

namespace UsersAPI.Tests.Helpers
{
  public static class TestHelper
  {
    /// <summary>
    /// Método para criar um client http da api de usuários
    /// </summary>
    public static HttpClient CreateClient
        => new WebApplicationFactory<Program>().CreateClient();

    /// <summary>
    /// Método para criar um client http não autorizado da api de usuários
    /// </summary>
    public static HttpClient CreateUnauthorizedClient()
    {
      var client = new WebApplicationFactory<Program>().CreateClient();
      // Simulando um client sem token de autenticação ou com um token inválido
      client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "invalid_token");
      return client;
    }

    /// <summary>
    /// Método para criar um client http autorizado da api de usuários
    /// </summary>
    public static async Task<HttpClient> CreateAuthorizedClient()
    {
      var client = new WebApplicationFactory<Program>().CreateClient();

      // Dados de login de exemplo
      var request = new LoginRequestDto
      {
        Email = "admin@example.com",
        Password = "@Admin123"
      };

      var content = CreateContent(request);

      // Fazendo a requisição de login para obter o token JWT
      var loginResponse = await client.PostAsync("api/auth/login", content);
      loginResponse.EnsureSuccessStatusCode();

      var loginResult = ReadResponse<LoginResponseDto>(loginResponse);
      var token = loginResult.AccessToken;

      // Configurando o token JWT no cabeçalho de autorização
      client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

      return client;
    }

    /// <summary>
    /// Método para serializar o conteudo
    /// da requisição que será enviada para um serviço
    /// </summary>
    public static StringContent CreateContent<TRequest>(TRequest request)
        => new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

    /// <summary>
    /// Método para deserializar o retorno obtido
    /// pela execução de um serviço
    /// </summary>
    public static TResponse ReadResponse<TResponse>(HttpResponseMessage message)
        => JsonConvert.DeserializeObject<TResponse>(message.Content.ReadAsStringAsync().Result);

  }
}
