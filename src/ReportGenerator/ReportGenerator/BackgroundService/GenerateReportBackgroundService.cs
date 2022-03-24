using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportGenerator.Config;
using ReportGenerator.Service;

namespace ReportGenerator.BackgroundService;


public class GenerateReportBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly RabbitMQConfiguration _config;
    private readonly GenerateReportService _service;
    private ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;

    public GenerateReportBackgroundService(IOptions<RabbitMQConfiguration> config, GenerateReportService service)
    {
        _config = config.Value;
        _service = service;

        this.Start();
    }


    public void Start()
    {
        //Starting Connection
        this._connectionFactory = new ConnectionFactory() { Uri = new Uri(this._config.Host) };
        this._connection = _connectionFactory.CreateConnection();
        this._channel = _connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _channel.QueueDeclare(queue: _config.QueueName, durable: true, exclusive: false, autoDelete: false);

        var consumer = new EventingBasicConsumer(_channel);
        _channel.BasicConsume(_config.QueueName, false, consumer);

        consumer.Received += Consumer_Received;

        return Task.CompletedTask;
    }

    private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
       var result = _service.CreateReport();

       if(result)
           _channel.BasicAck(e.DeliveryTag, false);
    }
    
}