using Shared.dto;
using Shared.Models;

namespace Backend.Services
{
    public interface ITodoApiServer {
        public List<TodoListItemReadDto> GetTodos();
        public TodoReadDto GetTodoById(int id);
        public TodoReadDto UpdateTodo(TodoWriteDto todo);
        public void UpdateTodoState(int id, bool isCompleted);
        public void DeleteTodo(int id);
        public TodoListItemReadDto AddTodo(TodoWriteDto todo);
    }
}
