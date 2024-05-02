using System.Reflection;
using SimpleRabbitmq.Handler;

namespace SimpleRabbitmq;

public static class Extension
{
    internal static string ExchangeName = "aspire_exchange";
    /// <summary>
    /// configure rabbitmq event bus
    /// </summary>
    /// <param name="webHostBuilder"></param>
    /// <param name="rabbitmqName">your aspire rabbitmq name</param>
    /// <param name="rabbitmqExchange">exchange name</param>
    public static void EventConfiguration(this WebApplicationBuilder webHostBuilder,string rabbitmqName,string rabbitmqExchange)
    {
        ExchangeName = rabbitmqName;
        webHostBuilder.Services.AddSingleton<IEventBus, RabbitmqService>();
        webHostBuilder.AddRabbitMQClient(rabbitmqName, (fa) =>
        {
            fa.HealthChecks = true;
            fa.Tracing = true;
        }, (f) =>
        {
            f.DispatchConsumersAsync = true;
        });
    }

    /// <summary>
    /// register all event handler in the app assemly
    /// </summary>
    /// <param name="app"></param>
    /// <exception cref="Exception"></exception>
    public static  async void RegisterRabbitmqEvent(this WebApplication app)
    {
        //get all assemblies
        var assemblies = Assembly.GetEntryAssembly();
        // find all types that implement IEventHandler
        var types = assemblies?.GetTypes().Where(t => typeof(IEventHandler).IsAssignableFrom(t) && !t.IsAbstract);
        // foreach type, subscribe to the event bus
       var eventBus =  app.Services.GetRequiredService<IEventBus>();
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<EventAttribute>() ?? throw new Exception("事件处理器没有配置EventAttribute");
            await eventBus.Subscribe(attribute.EventName, type);
        }
    }


}
