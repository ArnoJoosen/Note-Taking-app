using Shared.dto;
using Shared.Models;

namespace Backend.Services {
    public class MockApiTodoService : IApiTodoService {
        List<Todo> _todos = new();
        int currentTodoId = 0;

        public string BaseAddress { get; set; } = "http://localhost:5110";

        public MockApiTodoService() {
            _todos.Add(new Todo { Id = currentTodoId++, Title = "First todo", Description = "This is the first todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = true });
            _todos.Add(new Todo { Id = currentTodoId++, Title = "Second todo", Description = "This is the second todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
            _todos.Add(new Todo { Id = currentTodoId++, Title = "Third todo", Description = "This is the third todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
        }

        public Task<List<TodoListItemReadDto>> GetTodosAsync() {
            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            List<TodoListItemReadDto> todosList = new();
            foreach (var todo in _todos) {
                todosList.Add(new TodoListItemReadDto {
                    Id = todo.Id,
                    Title = todo.Title,
                    Detline = todo.Detline,
                    HasDetline = todo.HasDetline,
                    IsCompleted = todo.IsCompleted
                });
            }
            return Task.FromResult(todosList);
        }

        public Task<List<TodoListItemReadDto>> GetNotCompletedTodosAsync() {
            List<TodoListItemReadDto> todosList = new();
            _todos.Where(t => t.IsCompleted == false).ToList().ForEach(todo => {
                // create niew istance of todosList so that the frontend update the ui
                // and simulate the api call
                todosList.Add(new TodoListItemReadDto {
                    Id = todo.Id,
                    Title = todo.Title,
                    Detline = todo.Detline,
                    HasDetline = todo.HasDetline,
                    IsCompleted = todo.IsCompleted
                });
            });
            return Task.FromResult(todosList);
        }

        public Task<TodoReadDto> GetTodoByIdAsync(int id) {
            Todo? todo = _todos.Where(t => t.Id == id).First();
            if (todo == null) {
                throw new Exception("Todo not found");
            }
            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return Task.FromResult(new TodoReadDto {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Detline = todo.Detline,
                HasDetline = todo.HasDetline,
                IsCompleted = todo.IsCompleted
            });
        }

        public TodoReadDto GetTodoById(int id) {
            Todo? todo = _todos.Where(t => t.Id == id).First();
            if (todo == null) {
                throw new Exception("Todo not found");
            }
            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return new TodoReadDto {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Detline = todo.Detline,
                HasDetline = todo.HasDetline,
                IsCompleted = todo.IsCompleted
            };
        }

        public Task<TodoReadDto> UpdateTodoAsync(TodoWriteDto todo, int id) {
            if (todo == null) {
                throw new Exception("Todo not found");
            }

            Todo? existingTodo = _todos.Find(t => t.Id == id);
            if (existingTodo == null) {
                throw new Exception("Todo not found");
            }

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.Detline = todo.Detline;
            existingTodo.HasDetline = todo.HasDetline;
            existingTodo.IsCompleted = todo.IsCompleted;

            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return Task.FromResult(new TodoReadDto {
                Id = existingTodo.Id,
                Title = existingTodo.Title,
                Description = existingTodo.Description,
                Detline = existingTodo.Detline,
                HasDetline = existingTodo.HasDetline,
                IsCompleted = existingTodo.IsCompleted
            });
        }

        public Task UpdateTodoStateAsync(int id, bool isCompleted) {
            Todo? todo = _todos.Find(t => t.Id == id);
            if (todo == null) {
                throw new Exception("Todo not found");
            }
            todo.IsCompleted = isCompleted;
            return Task.CompletedTask;
        }

        public Task DeleteTodoAsync(int id) {
            Todo? todo = _todos.Find(t => t.Id == id);
            if (todo == null) {
                return Task.CompletedTask;
            }
            _todos.Remove(todo);
            return Task.CompletedTask;
        }

        public Task<TodoListItemReadDto> CreateTodoAsync(TodoWriteDto todo) {
            Todo todonew = new Todo {
                Id = currentTodoId++,
                Title = todo.Title,
                Description = todo.Description,
                Detline = todo.Detline,
                HasDetline = todo.HasDetline,
                IsCompleted = todo.IsCompleted
            };
            _todos.Add(todonew);
            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return Task.FromResult(new TodoListItemReadDto {
                Id = todonew.Id,
                Title = todonew.Title,
                Detline = todonew.Detline,
                HasDetline = todonew.HasDetline,
                IsCompleted = todonew.IsCompleted
            });
        }
    }
}
