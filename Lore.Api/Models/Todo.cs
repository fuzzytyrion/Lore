using Microsoft.EntityFrameworkCore;

namespace Lore.Api.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions options) : base(options) { }
        public DbSet<Todo> Todos { get; set; }
    }
}
