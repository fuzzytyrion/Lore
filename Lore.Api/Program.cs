using Lore.Api;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
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

app.MapGet("/", () => "Hello World!");
app.MapGet("/todos/{id}", (int id) => Db.GetTodo(id));
app.MapGet("/todos", () => Db.GetTodos());
app.MapPost("/todos", (Todo todo) => Db.CreateTodo(todo));
app.MapPut("/todos", (Todo todo) => Db.UpdateTodo(todo));
app.MapDelete("/todos/{id}", (int id) => Db.RemoveTodo(id));

app.Run();
