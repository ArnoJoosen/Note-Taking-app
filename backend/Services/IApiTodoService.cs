using Shared.dto;

namespace Backend.Services
{
    public interface IApiTodoService {
        public List<TodoListItemReadDto> GetTodos();
        public List<TodoListItemReadDto> GetNotCompletedTodos();
        public TodoReadDto GetTodoById(int id);
        public TodoListItemReadDto CreateTodo(TodoWriteDto todo);
        public TodoReadDto UpdateTodo(TodoWriteDto todo, int id);
        public void UpdateTodoState(int id, bool isCompleted);
        public void DeleteTodo(int id);
        
    }
}
