using Shared.Models;
using Server.Contexts;

namespace Server.Repositories {
    public class TodoRepo : ITodoRepo {

        private readonly SQLContext _context;
        public TodoRepo(SQLContext context) {
            _context = context;
        }

        public IEnumerable<Todo> GetAllTodos() {
            return _context.Todos.ToList();
        }
        public IEnumerable<Todo> GetNotCompletedTodos() {
            return _context.Todos.Where(t => t.IsCompleted == false).ToList();
        }
        public Todo GetTodoById(int id) {
            var item = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (item != null) {
                return item;
            }
            throw new System.Exception("Todo not found");
        }
        public Todo CreateTodo(string Title, string Description, DateTime Detline, bool HasDetline) {
            var newTodo = new Todo {
                Title = Title,
                Description = Description,
                IsCompleted = false,
                Detline = Detline,
                HasDetline = HasDetline,
                CreatedAt = DateTime.Now
            };
            _context.Todos.Add(newTodo);
            _context.SaveChanges();
            return newTodo;
        }
        public void UpdateTodoById(Todo uTodo) {
            var item = _context.Todos.FirstOrDefault(t => t.Id == uTodo.Id);
            if (item != null) {
                item.Title = uTodo.Title;
                item.Description = uTodo.Description;
                item.IsCompleted = uTodo.IsCompleted;
                item.Detline = uTodo.Detline;
                item.HasDetline = uTodo.HasDetline;
                _context.SaveChanges();
                return;
            }
            throw new System.Exception("Todo not found");
        }
        public void UpdateTodoState(int id, bool isCompleted) {
            var item = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (item != null) {
                item.IsCompleted = isCompleted;
                _context.SaveChanges();
                return;
            }
            throw new System.Exception("Todo not found");
        }
        public void DeleteTodoById(int id) {
            var item = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (item != null) {
                _context.Todos.Remove(item);
                _context.SaveChanges();
                return;
            }
            throw new System.Exception("Todo not found");
        }
    }
}
