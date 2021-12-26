using Lore.Api.Models;
using Microsoft.EntityFrameworkCore;

public static class TodoApi
{
    public static void MapTodoRoutes(this IEndpointRouteBuilder app)
    {
                
        app.MapGet("/todos", async (TodoDb db) => await db.Todos.ToListAsync())
            .Produces<List<Todo>>(StatusCodes.Status200OK)
            .WithName("GetAllTodos")
            .WithTags("Getters", "Todos");

        app.MapGet("/todos/{id}", async (TodoDb db, int id) => await db.Todos.FindAsync(id))
            .Produces<Todo>(StatusCodes.Status200OK)
            .WithName("GetTodo")
            .WithTags("Getters", "Todos");

        app.MapPost("/todos", async (TodoDb db, Todo todo) =>
        {
            await db.Todos.AddAsync(todo);
            db.SaveChanges();
            return Results.Created($"/todos/{todo.Id}", todo);
        }).Produces<Todo>(StatusCodes.Status201Created)
            .WithName("CreateTodo")
            .WithTags("Setters", "Todos");

        app.MapPut("/todos/{id}", async (TodoDb db, Todo updateTodo, int id) =>
        {
            var todo = await db.Todos.FindAsync(id);
            if (todo == null) return Results.NotFound();
            todo.Name = updateTodo.Name;
            todo.Description = updateTodo.Description;
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).Produces<Todo>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("UpdateTodo")
            .WithTags("Setters", "Todos");

        app.MapDelete("/todos/{id}", async (TodoDb db, int id) =>
        {
            var todo = await db.Todos.FindAsync(id);
            if (todo == null) return Results.NotFound();
            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
            return Results.Ok();
        }).Produces<Todo>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("DeleteTodo")
            .WithTags("Setters", "Todos");

    }
}