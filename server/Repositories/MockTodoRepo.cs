using Shared.Models;

namespace NoteTakingServer.Repositories {
    public class MockTodoRepo : ITodoRepo {
        List<Todo> todos = new List<Todo> {
            new Todo { Id = 1, Title = "First todo", Description = "This is the first todo", Detline=DateTime.Now, HasDetline=false, IsCompleted = false },
            new Todo { Id = 2, Title = "Second todo", Description = "This is the second todo", Detline=DateTime.Now, HasDetline=false, IsCompleted = false },
            new Todo { Id = 3, Title = "Third todo", Description = "This is the third todo", Detline=DateTime.Now, HasDetline=false, IsCompleted = false }
        };

        public IEnumerable<Todo> GetAllTodos() {
            return todos;
        }

        public Todo GetTodoById(int id) {
            var todo = todos.FirstOrDefault(todo => todo.Id == id);
            if (todo == null) {
                throw new KeyNotFoundException($"Todo with Id {id} not found.");
            }
            return todo;
        }

        public void CreateTodo() {
            todos.Add(new Todo { Id = 4, Title = "Fourth todo", Description = "This is the fourth todo", IsCompleted = false });
        }

        public void UpdateTodoById(int id) {
            var todo = todos.FirstOrDefault(todo => todo.Id == id);
            if (todo != null) {
                todo.Title = "Updated todo";
                todo.Description = "This is the updated todo";
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