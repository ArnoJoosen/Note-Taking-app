using Shared.Models;
using Server.Contexts;

namespace Server.Repositories {
    public class TodoRepo : ITodoRepo {

        private readonly TodoContext _context;
        public TodoRepo(TodoContext context) {
            _context = context;
        }

        public IEnumerable<Todo> GetAllTodos() {
            return _context.Todos.ToList();
        }
        public IEnumerable<Todo> GetNotCompletedTodos() {
            throw new System.NotImplementedException();
        }
        public Todo GetTodoById(int id) {
            try {
                return _context.Todos.FirstOrDefault(t => t.Id == id);
            } catch (System.Exception) {
                return null;
            }
        }
        public Todo CreateTodo(string Title, string Description, DateTime Detline, bool HasDetline) {
            throw new System.NotImplementedException();
        }
        public void UpdateTodoById(Todo uTodo) {
            throw new System.NotImplementedException();
        }
        public void UpdateTodoState(int id, bool isCompleted) {
            throw new System.NotImplementedException();
        }
        public void DeleteTodoById(int id) {
            throw new System.NotImplementedException();
        }
    }
}
