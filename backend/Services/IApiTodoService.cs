using Shared.dto;

namespace Backend.Services {
    public interface IApiTodoService {
        public Task<List<TodoListItemReadDto>> GetTodosAsync();
        public Task<List<TodoListItemReadDto>> GetNotCompletedTodosAsync();
        public Task<TodoReadDto> GetTodoByIdAsync(int id);
        public TodoReadDto GetTodoById(int id);
        public Task<TodoListItemReadDto> CreateTodoAsync(TodoWriteDto todo);
        public Task<TodoReadDto> UpdateTodoAsync(TodoWriteDto todo, int id);
        public Task UpdateTodoStateAsync(int id, bool isCompleted);
        public Task DeleteTodoAsync(int id);
    }
}