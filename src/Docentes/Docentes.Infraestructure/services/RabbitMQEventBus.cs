using System.Text;
using System.Text.Json;
using Docentes.Application.Services;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Docentes.Infrastructure.Services;

public class RabbitMQEventBus : IEventBus
{
    private IConnection _connection;
    private IModel _channel;
    private readonly IPublisher _publisher;

    public RabbitMQEventBus(string url, IPublisher publisher)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(url)
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _publisher = publisher;
    }

    public void Consume<T>() where T : class
    {
        var queueName = typeof(T).Name;
        _channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var @event = JsonSerializer.Deserialize<T>(message);

            if (@event != null)
            {
                try
                {
                    await _publisher.Publish(@event);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception)
                {
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }
            }
        };
        _channel.BasicConsume(queueName, autoAck: false, consumer: consumer);
    }

    public void Publish<T>(T @event) where T : class
    {
        var queueName = typeof(T).Name;
        _channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }
}