using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Services;
using Shared.Models;

namespace Backend.ViewModels
{
    public class TodoEditViewModel {
        ITodoApiServer _api;
        int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public bool HasDetline { get; set; } = false;

        public DateTime DetLine {  get; set; } = DateTime.Now;

        public bool IsCompleted { get; set; } = false;

        public TodoEditViewModel(ITodoApiServer api, int id) {
            _api = api;
            Id = id;
            Todo todo = _api.GetTodoById(id);
            Title = todo.Title;
            Description = todo.Description;
            HasDetline = todo.HasDetline;
        }

        public void Save() {
            _api.UpdateTodo(new Todo { Id = Id, Title = Title, Description = Description, HasDetline = HasDetline, Detline = DetLine, IsCompleted = IsCompleted });
        }
    }
}
