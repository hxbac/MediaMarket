using MediaMarket.Application.Configs;
using MediaMarket.Application.Contracts.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MediaMarket.Infrastructure.Messaging.Publishers
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly RabbitMqConfig _rabbitMqConfig;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IChannel _channel;
        private ILogger<RabbitMqEventPublisher> _logger;

        public RabbitMqEventPublisher(IOptions<RabbitMqConfig> rabbitMqConfig, ILogger<RabbitMqEventPublisher> logger)
        {
            _rabbitMqConfig = rabbitMqConfig.Value;
            _logger = logger;

            _connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitMqConfig.HostName,
                Port = _rabbitMqConfig.Port,
                UserName = _rabbitMqConfig.Username,
                Password = _rabbitMqConfig.Password
            };
        }

        public void Dispose()
        {
            _channel?.CloseAsync().Wait();
            _channel?.Dispose();
            _connection?.CloseAsync().Wait();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMqEventPublisher dispose connection.");
        }

        public async Task Initialize()
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
        }

        public async Task PublishAsync<T>(T @event, string queueKey)
        {
            if (!_rabbitMqConfig.Queues.TryGetValue(queueKey, out var queueName))
            {
                throw new ArgumentException($"Queue with key '{queueKey}' is not configured.");
            }

            await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);

            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, mandatory: true, body: body);
        }
    }
}
