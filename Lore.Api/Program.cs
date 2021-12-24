using Lore.Api;
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

app.MapGet("/", () => "Hello World!").ExcludeFromDescription();

app.MapGet("/todos", async (TodoDb db) => await db.Todos.ToListAsync())
    .Produces<List<Todo>>()
    .WithName("GetAllTodos")
    .WithTags("Getters", "Todos");

app.MapGet("/todos/{id}", async (TodoDb db, int id) => await db.Todos.FindAsync(id))
    .Produces<Todo>()
    .WithName("GetTodo")
    .WithTags("Getters", "Todos");

app.MapPost("/todos", async (TodoDb db, Todo todo) =>
{
    await db.Todos.AddAsync(todo);
    db.SaveChanges();
    return Results.Created($"/todos/{todo.Id}", todo);
}).WithName("CreateTodo")
    .WithTags("Setters", "Todos");

app.MapPut("/todos/{id}", async (TodoDb db, Todo updateTodo, int id) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo == null) return Results.NotFound();
    todo.Name = updateTodo.Name;
    todo.Description = updateTodo.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
}).WithName("UpdateTodo")
    .WithTags("Setters", "Todos");

app.MapDelete("/todos/{id}", async (TodoDb db, int id) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo == null) return Results.NotFound();
    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.Ok();
}).WithName("DeleteTodo")
    .WithTags("Setters", "Todos");

app.Run();
