using Shared.dto;
using Shared.Models;

namespace Server.Repositories {
    public interface ITodoRepo {
        public IEnumerable<Todo> GetAllTodos();
        public IEnumerable<Todo> GetNotCompletedTodos();
        public Todo GetTodoById(int id);
        public Todo CreateTodo(string Title, string Description, DateTime Detline, bool HasDetline);
        public void UpdateTodoById(Todo uTodo);
        public void UpdateTodoState(int id, bool isCompleted);
        public void DeleteTodoById(int id);
    }
}