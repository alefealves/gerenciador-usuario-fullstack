using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Messages.Models;
using UsersAPI.Infra.Messages.Settings;

namespace UsersAPI.Infra.Messages.Consumers
{
  public class UserMessageConsumer : BackgroundService
  {
    private readonly IServiceProvider? _serviceProvider;
    //private readonly EmailMessageService? _emailMessageService;
    private readonly RabbitMQSettings? _rabbitmqSettings;

    private IConnection _connection;
    private IModel _model;

    public UserMessageConsumer(IServiceProvider? serviceProvider, IOptions<RabbitMQSettings>? rabbitmqSettings)
    {
      _serviceProvider = serviceProvider;
      //_emailMessageService = emailMessageService;
      _rabbitmqSettings = rabbitmqSettings.Value;

      //Conectando na servidor do RabbitMQ
      //var factory = new ConnectionFactory { Uri = new Uri(_rabbitmqSettings?.Url) };
      var factory = new ConnectionFactory() { HostName = _rabbitmqSettings?.Url };
      _connection = factory.CreateConnection();

      //Conectando na fila do RabbitMQ
      _model = _connection.CreateModel();
      _model.QueueDeclare(
          queue: _rabbitmqSettings?.Queue,
          durable: true,
          autoDelete: false,
          exclusive: false,
          arguments: null
          );
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      //Objeto utilizado para ler e processar a fila
      var consumer = new EventingBasicConsumer(_model);

      //Criando o mecanismo para ler cada item da fila
      consumer.Received += async (sender, args) =>
      {
        //Lendo e deserializando o conteúdo do item obtida da fila
        var payload = Encoding.UTF8.GetString(args.Body.ToArray());
        var userMessaeVO = JsonConvert.DeserializeObject<UserMessageVO>(payload);

        //Processando o item
        using (var scope = _serviceProvider.CreateScope())
        {
          var messageRequestModel = new MessagesRequestModel
          {
            Email = userMessaeVO.Email,
            Subject = userMessaeVO.Subject,
            Body = userMessaeVO.Body
          };

          //enviando a mensagem para o email do usuário
          //await _emailMessageService?.SendMessage(messageRequestModel);

          //Removendo o item da fila
          _model.BasicAck(args.DeliveryTag, false);
          //Console.WriteLine("Email processado com sucesso!");
        }
      };

      //Executando a leitura da fila
      _model.BasicConsume(_rabbitmqSettings.Queue, false, consumer);

    }
  }
}
