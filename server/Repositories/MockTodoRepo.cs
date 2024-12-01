using Shared.Models;

namespace Server.Repositories {
    public class MockTodoRepo : ITodoRepo {
        int _currentID = 0;
        List<Todo> todos = new();

        public MockTodoRepo() {
            todos.Add(new Todo { Id = _currentID++, Title = "First todo", Description = "This is the first todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = true });
            todos.Add(new Todo { Id = _currentID++, Title = "Second todo", Description = "This is the second todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
            todos.Add(new Todo { Id = _currentID++, Title = "Third todo", Description = "This is the third todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
            todos.Add(new Todo { Id = _currentID++, Title = "Fourth todo", Description = "This is the fourth todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
            todos.Add(new Todo { Id = _currentID++, Title = "Fifth todo", Description = "This is the fifth todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
        }

        public IEnumerable<Todo> GetAllTodos() {
            return todos;
        }

        public IEnumerable<Todo> GetNotCompletedTodos() {
            return todos.Where(todo => !todo.IsCompleted);
        }

        public Todo GetTodoById(int id) {
            var todo = todos.FirstOrDefault(todo => todo.Id == id);
            if (todo == null) {
                throw new KeyNotFoundException($"Todo with Id {id} not found.");
            }
            return todo;
        }

        public Todo CreateTodo(string Title, string Description, DateTime Detline, bool HasDetline) {
            Todo todo = new Todo { Id = _currentID++, Title = Title, Description = Description, IsCompleted = false, CreatedAt = DateTime.Now, Detline = Detline, HasDetline = HasDetline };
            todos.Add(todo);
            return todo;
        }

        public void UpdateTodoById(Todo uTodo) {
            if (uTodo == null) {
                throw new ArgumentNullException(nameof(uTodo));
            }
            var todo = todos.FirstOrDefault(todo => todo.Id == uTodo.Id);
            if (todo != null) {
                todo.Title = uTodo.Title;
                todo.Description = uTodo.Description;
                todo.Detline = uTodo.Detline;
                todo.HasDetline = uTodo.HasDetline;
                todo.IsCompleted = uTodo.IsCompleted;
            } else {
                throw new KeyNotFoundException($"Todo with Id {uTodo.Id} not found.");
            }
        }

        public void UpdateTodoState(int id, bool isCompleted) {
            var todo = todos.FirstOrDefault(todo => todo.Id == id);
            if (todo != null) {
                todo.IsCompleted = isCompleted;
            } else {
                throw new KeyNotFoundException($"Todo with Id {id} not found.");
            }
        }

        public void DeleteTodoById(int id) {
            var todo = todos.FirstOrDefault(todo => todo.Id == id);
            if (todo != null) {
                todos.Remove(todo);
            } else {
                throw new KeyNotFoundException($"Todo with Id {id} not found.");
            }
        }
    }
}