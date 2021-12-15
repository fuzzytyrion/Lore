namespace Lore.Api
{
    public class Db
    {
        private static List<Todo> _todos = new List<Todo>()
        {
            new Todo() { Name = "Do this", Id = 1 },
            new Todo() { Name = "Do that", Id=2 },
            new Todo() { Name ="And the other thing", Id =3 }
        };

        public static List<Todo> GetTodos()
        {
            return _todos;
        }

        public static Todo? GetTodo(int Id)
        {
            return _todos.SingleOrDefault(t => t.Id == Id);
        }

        public static Todo CreateTodo(Todo todo)
        {
            _todos.Add(todo);
            return todo;    
        }

        public static Todo UpdateTodo(Todo update)
        {
            _todos[update.Id] = update;
            return update;
        }

        public static void RemoveTodo(int id)
        {
            _todos = _todos.FindAll(t => t.Id != id).ToList();
        }

    }
}
