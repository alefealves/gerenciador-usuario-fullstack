using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersAPI.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace UsersAPI.Infra.IoC.Extensions
{
  public static class DbContextExtension
  {
    public static IServiceCollection AddDbContextConfig
        (this IServiceCollection services, IConfiguration configuration)
    {
      //capturando o parâmetro DatabaseProvider
      var databaseProvider = configuration.GetSection("DatabaseProvider").Value;

      //verificando o tipo de provider de banco de dados
      switch (databaseProvider)
      {
        case "SqlServer":
          services.AddDbContext<DataContext>(options =>
          {
            options.UseSqlServer(configuration.GetConnectionString("DBUsers"));
          });
          break;

        case "InMemory":
          services.AddDbContext<DataContext>(options =>
          {
            options.UseInMemoryDatabase(databaseName: "DBUsers");
          });
          break;
      }

      return services;
    }
  }
}
