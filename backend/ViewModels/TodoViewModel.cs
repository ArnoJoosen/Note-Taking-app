using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared.Models;
using Backend.Services;

namespace Backend.ViewModels
{
    public class TodoViewModel
    {
        public ObservableCollection<Todo> ObservableTodoItems { get; set; } = new ObservableCollection<Todo>();
        public String InputTitle { get; set; } = "";

        private readonly ITodoApiServer _api = (ITodoApiServer)new MockTodoApiService();

        public TodoViewModel()
        {

        }

        public void UpdateTodoList()
        {
            ObservableTodoItems.Clear();
            var todos = _api.GetTodos();
            foreach (var todo in todos)
            {
                ObservableTodoItems.Add(todo);
            }
        }

        public void addTodo()
        {
            if (InputTitle == "") {
                return;
            }
            Todo todo = new Todo
            {
                Id = 0,
                Title = InputTitle,
                Description = "",
                Detline = DateTime.Now,
                HasDetline = false,
                IsCompleted = false
            };
            ObservableTodoItems.Add(_api.AddTodo(todo));
        }

        public void removeTodo(int id)
        {
            _api.DeleteTodo(id);
        }
    }
}
