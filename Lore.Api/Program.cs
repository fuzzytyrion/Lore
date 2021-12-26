using Lore.Api.Models;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Todos") ?? "Data Source=Todos.db";
builder.Services.AddSqlite<TodoDb>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<TodoDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lore API", Description = "Adding some knowledge to my brain", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lore API V1");
});
app.MapHelloWorldRoutes();
app.MapTodoRoutes();

app.Run();
