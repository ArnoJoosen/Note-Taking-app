using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Services;
using Shared.dto;
using Shared.Models;

namespace Backend.ViewModels {
    public class TodoEditViewModel : INotifyPropertyChanged {
        IApiService _api;
        int ItemId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        private bool _hasDetline = false;
        public bool HasDetline {
            get { return _hasDetline; }
            set {
                if (_hasDetline != value) {
                    _hasDetline = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasDetline)));
                }
            }
        }

        public DateTime DetLine { get; set; } = DateTime.Now;

        public bool IsCompleted { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public TodoEditViewModel(IApiService api, int id) {
            _api = api;
            ItemId = id;
            TodoReadDto todo = _api.GetTodoById(id);
            Title = todo.Title;
            Description = todo.Description;
            HasDetline = todo.HasDetline;
            IsCompleted = todo.IsCompleted;
        }

        public void Save() {
            _api.UpdateTodo(new TodoWriteDto {
                Id = ItemId,
                Title = Title,
                Description = Description,
                HasDetline = HasDetline,
                Detline = DetLine,
                IsCompleted = IsCompleted });
        }
    }
}
