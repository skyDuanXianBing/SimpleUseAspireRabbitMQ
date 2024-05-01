using Microsoft.Extensions.Configuration;
using SimpleRabbitmq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.EventConfiguration("rabbitmq","myexchange");
// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.RegisterRabbitmqEvent();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapControllers();

app.Run();
