using Shared.Models;

namespace Backend.Services
{
    public interface ITodoApiServer
    {
        public List<Todo> GetTodos();
        public Todo GetTodoById(int id);
        public Todo UpdateTodo(Todo todo);
        public void DeleteTodo(int id);
        public Todo AddTodo(Todo todo);
    }
}
