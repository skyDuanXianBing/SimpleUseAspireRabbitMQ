using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SimpleRabbitmq.Handler;
using System.Text;
using System.Text.Json;

namespace SimpleRabbitmq;

public class RabbitmqService : IEventBus, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchangeName = Extension.ExchangeName;

    public RabbitmqService(IConnection connection)
    {
        // 需要重试的操作
        _connection = connection;
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);


    }


    public Task Publish(string eventName, object? message)
    {
        byte[] body;
        if (message == null)
        {
            body = [];
        }
        else
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            body = JsonSerializer.SerializeToUtf8Bytes(message, message.GetType(), options);
        }

        var properties = _channel.CreateBasicProperties();
        properties.DeliveryMode = 2; // 持久化
        _channel.QueueDeclare(eventName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(eventName, _exchangeName, eventName);
        _channel.BasicPublish(_exchangeName, eventName, properties, body);
        return Task.CompletedTask;
    }

    public Task Subscribe(string eventName, Type handlerType)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) => await Consumer_Received(model, ea, eventName, handlerType);
        _channel.QueueDeclare(eventName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(eventName, _exchangeName, eventName);
        _channel.BasicConsume(eventName, false, consumer);

        return Task.CompletedTask;
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs, string eventName, Type handlerType)
    {
        //var eventName = eventArgs.RoutingKey;//这个框架中，就是用eventName当RoutingKey
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);//框架要求所有的消息都是字符串的json
        var handler = Activator.CreateInstance(handlerType) as IEventHandler ?? throw new ArgumentException("Handler type is not implement IEventHandler interface");

        await handler.Handle(eventName, message);
        _channel.BasicAck(eventArgs.DeliveryTag, false);
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
    }
}
