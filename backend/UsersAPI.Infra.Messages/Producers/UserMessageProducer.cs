using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using UsersAPI.Domain.Interfaces.Messages;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Messages.Settings;

namespace UsersAPI.Infra.Messages.Producers
{
  public class UserMessageProducer : IUserMessageProducer
  {
    private readonly RabbitMQSettings? _rabbitmqSettings;

    //Pegando os valores definidos no appsettings.json
    public UserMessageProducer(IOptions<RabbitMQSettings?> rabbitmqSettings)
    {
      _rabbitmqSettings = rabbitmqSettings.Value;
    }

    public void Send(UserMessageVO userMessage)
    {
      //Conectando no servidor do RabbitMQ
      //var connectionFactory = new ConnectionFactory() { Uri = new Uri(_rabbitmqSettings?.Url) };
      var connectionFactory = new ConnectionFactory() { HostName = _rabbitmqSettings?.Url };
      using (var connection = connectionFactory.CreateConnection())
      {
        using (var model = connection.CreateModel())
        {
          //criando a fila
          model.QueueDeclare(
              queue: _rabbitmqSettings.Queue, //nome da fila
              durable: true, //não apagar as filas ao desligar ou reiniciar o broker
              autoDelete: false, //apagar ou não a fila quando estiver vazia
              exclusive: false, //fila exclusiva ou não para uma única aplicação
              arguments: null
              );

          //gravar a mensagem na fila
          model.BasicPublish(
              exchange: string.Empty,
              routingKey: _rabbitmqSettings.Queue,
              basicProperties: null,
              body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userMessage))
              );

          //Console.WriteLine("Email adicionado a fila!");
        }
      }
    }
  }
}
