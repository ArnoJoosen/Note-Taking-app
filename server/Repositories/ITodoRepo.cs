using Shared.Models;

namespace NoteTakingServer.Repositories {
    public interface ITodoRepo {
        public IEnumerable<Todo> GetAllTodos();

        public Todo GetTodoById(int id);

        public void CreateTodo();

        public void UpdateTodoById(int id);

        public void DeleteTodoById(int id);
    }   
}