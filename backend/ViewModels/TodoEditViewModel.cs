using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Services;
using Shared.dto;
using Shared.Models;

namespace Backend.ViewModels {
    public class TodoEditViewModel {
        ITodoApiServer _api;
        int ItemId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public bool HasDetline { get; set; } = false;

        public DateTime DetLine { get; set; } = DateTime.Now;

        public bool IsCompleted { get; set; } = false;

        public TodoEditViewModel(ITodoApiServer api, int id) {
            _api = api;
            ItemId = id;
            TodoReadDto todo = _api.GetTodoById(id);
            Title = todo.Title;
            Description = todo.Description;
            HasDetline = todo.HasDetline;
            IsCompleted = todo.IsCompleted;
        }

        public void Save() {
            _api.UpdateTodo(new TodoWriteDto { Id = ItemId, Title = Title, Description = Description, HasDetline = HasDetline, Detline = DetLine, IsCompleted = IsCompleted });
        }
    }
}
