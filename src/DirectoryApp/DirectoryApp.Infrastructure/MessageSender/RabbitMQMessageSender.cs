using DirectoryApp.Application.ConfigurationModel;
using DirectoryApp.Application.MessageSender;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DirectoryApp.Infrastructure.MessageSender;

public class RabbitMQMessageSender : IMessageSender
{
    private ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;
    private readonly RabbitMQConfiguration _config;


    private bool IsConnectionClosed() => _connectionFactory is null || _connection is null || _channel is null || !_connection.IsOpen;
    
    public RabbitMQMessageSender(RabbitMQConfiguration config) 
    {
        this._config = config;
        this.InitializeConnection(config);
    }


    public Task<bool> SendGenerateReportMessage(object @object)
    {
        //Does Not Matter What is The Sending Message. We Just Need Trigger The Consumer
        if (this.IsConnectionClosed())
        {
            //ReInitializing The Connection
            this.InitializeConnection(this._config);
        }

        var publishOptions = _channel.CreateBasicProperties();
        publishOptions.Persistent = true;

        try
        {
            byte[] message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@object));
            _channel.BasicPublish(exchange: _config.ExchangeName, routingKey: "generate-report-routekey", basicProperties: publishOptions, body : message);
            return Task.FromResult<bool>(true);
        }
        catch (Exception ex)
        {
            return Task.FromResult<bool>(false);
        }
        finally
        {
            this.DisposeConnection();
        }
    }


    public void InitializeConnection(RabbitMQConfiguration config)
    {
        _connectionFactory = new ConnectionFactory() { Uri = new Uri(config.Host) };
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();       
    }


    public void InitializeExchangesAndQueues(RabbitMQConfiguration config)
    {
        _channel.ExchangeDeclare(exchange: config.ExchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false);
        _channel.QueueDeclare(queue: config.QueueName, durable: true, exclusive: false, autoDelete: false);
        _channel.QueueBind(queue: config.QueueName, exchange: config.ExchangeName, routingKey: config.QueueName);
    }


    private void DisposeConnection()
    {
        this._channel.Dispose();
        this._connection.Dispose();     
    }


    
}