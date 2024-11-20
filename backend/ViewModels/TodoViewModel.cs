using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using backend.Models;

namespace backend.ViewModels
{
    public class TodoViewModel {
        public ObservableCollection<Todo> TodoItems { get; set; } = new ObservableCollection<Todo>();

        TodoViewModel() {
            TodoItems.Add(new Todo { Id = 1, Title = "First todo", Description = "This is the first todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
            TodoItems.Add(new Todo { Id = 2, Title = "Second todo", Description = "This is the second todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
            TodoItems.Add(new Todo { Id = 3, Title = "Third todo", Description = "This is the third todo", Detline = DateTime.Now, HasDetline = false, IsCompleted = false });
        }

        void addTodo(Todo item) {
            TodoItems.Add(item);
        }

        void removeTodo(int Id) {
            
        }
    }
}
