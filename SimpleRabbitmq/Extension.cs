using System.Reflection;
using SimpleRabbitmq.Handler;

namespace SimpleRabbitmq;

public static class Extension
{
    internal static string ExchangeName = "aspire_exchange";
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

    public static  async Task RegisterRabbitmqEvent(this WebApplication app)
    {
        //拿到全部的程序集
        var assemblies = Assembly.GetEntryAssembly();
        // 查找实现ieventhandl接口的类
        var types = assemblies?.GetTypes().Where(t => typeof(IEventHandler).IsAssignableFrom(t) && !t.IsAbstract);
        // 遍历找到对应的事件处理器
       var eventBus =  app.Services.GetRequiredService<IEventBus>();
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<EventAttribute>() ?? throw new Exception("事件处理器没有配置EventAttribute");
            await eventBus.Subscribe(attribute.EventName, type);
        }
    }


}
