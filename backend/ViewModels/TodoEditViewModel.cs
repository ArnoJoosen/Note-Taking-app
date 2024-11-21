using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;

namespace Backend.ViewModels
{
    public class TodoEditViewModel {

        int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public bool HasDetline { get; set; } = false;

        public DateTime DetLine {  get; set; } = DateTime.Now;

        public bool IsCompleted { get; set; } = false;

        public TodoEditViewModel(Todo todo) {
            Id = todo.Id;
            todo.Title = Title;
            todo.Description = Description;
            todo.HasDetline = HasDetline;
            todo.Detline = DetLine;
            todo.IsCompleted = IsCompleted;
        }
    }
}
