using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;

namespace Backend.Services {
    public class MockTodoApiService : ITodoApiServer {
        List<Todo> _todos = new();
        int currentId = 0;

        public MockTodoApiService() {
            _todos.Add(new Todo { Id = currentId++, Title = "First todo", Description = "This is the first todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = true });
            _todos.Add(new Todo { Id = currentId++, Title = "Second todo", Description = "This is the second todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
            _todos.Add(new Todo { Id = currentId++, Title = "Third todo", Description = "This is the third todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
        }

        public List<Todo> GetTodos() {
        // create niew istance of todosList so that the frontend update the ui
        // and simulate the api call
            List<Todo> todosList = new();
            foreach (var todo in _todos) {
                todosList.Add(new Todo {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Detline = todo.Detline,
                    HasDetline = todo.HasDetline,
                    IsCompleted = todo.IsCompleted
                });
            }
            return todosList;
        }

        public Todo GetTodoById(int id) {
            Todo? todo = _todos.Where(t => t.Id == id).First();
            if (todo == null)
            {
                // TODO add error
            }
            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return new Todo {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Detline = todo.Detline,
                HasDetline = todo.HasDetline,
                IsCompleted = todo.IsCompleted
            };
        }

        public Todo UpdateTodo(Todo todo) {
            if (todo == null) {
                return todo; // TODO add error
            }

            Todo? existingTodo = _todos.Find(t => t.Id == todo.Id);
            if (existingTodo == null) {
                return todo; // TODO add error
            }

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.Detline = todo.Detline;
            existingTodo.HasDetline = todo.HasDetline;
            existingTodo.IsCompleted = todo.IsCompleted;

            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return new Todo
            {
                Id = existingTodo.Id,
                Title = existingTodo.Title,
                Description = existingTodo.Description,
                Detline = existingTodo.Detline,
                HasDetline = existingTodo.HasDetline,
                IsCompleted = existingTodo.IsCompleted
            };
        }

        public void DeleteTodo(int id) {
            Todo? todo = _todos.Find(t => t.Id == id);
            if (todo == null)
            {
                return;
            }
            _todos.Remove(todo);
        }

        public Todo AddTodo(Todo todo) {
            Todo todonew = new Todo {
                Id = currentId++,
                Title = todo.Title,
                Description = todo.Description,
                Detline = todo.Detline,
                HasDetline = todo.HasDetline,
                IsCompleted = todo.IsCompleted
            };
            _todos.Add(todonew);
            // create niew istance of todosList so that the frontend update the ui
            // and simulate the api call
            return new Todo {
                Id = todonew.Id,
                Title = todonew.Title,
                Description = todonew.Description,
                Detline = todonew.Detline,
                HasDetline = todonew.HasDetline,
                IsCompleted = todonew.IsCompleted
            };
        }
    }
}
