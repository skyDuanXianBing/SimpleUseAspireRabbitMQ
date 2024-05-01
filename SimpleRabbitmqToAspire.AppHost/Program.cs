var builder = DistributedApplication.CreateBuilder(args);

//var container = builder.AddContainer("rabbitmq", "rabbitmq").WithImageTag("3").WithHttpEndpoint(5672, 5672).WithHttpEndpoint(15672, 15672);
 var rabbitmq = builder.AddRabbitMQ("rabbitmq");



var apiService = builder.AddProject<Projects.SimpleRabbitmqToAspire_ApiService>("apiservice").WithReference(rabbitmq);

builder.AddProject<Projects.Test1>("test1").WithReference(rabbitmq);


builder.Build().Run();
