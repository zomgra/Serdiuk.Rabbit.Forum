using Serdiuk.Rabbit.API.Data.Di;
using Serdiuk.Rabbit.Di;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddElasticSearch(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddMapper();
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddListener();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
