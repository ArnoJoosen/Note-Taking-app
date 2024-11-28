using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shared.Models;
using Backend.Services;
using System.Windows.Input;
using Backend;

namespace Backend.ViewModels
{
    public class TodoViewModel
    {
        public ObservableCollection<Todo> ObservableTodoItems { get; set; } = new ObservableCollection<Todo>();
        public String InputTitle { get; set; } = "";

        private ITodoApiServer _api;

        public ICommand addCommand { get; private set; }
        public ICommand deleteCommand { get; private set; }
        public ICommand changeDonStateCommand { get; private set; }

        public TodoViewModel(ITodoApiServer api) {
            _api = api;
            addCommand = new DelegateCommand(p => AddTodo());
            deleteCommand = new DelegateCommand(p => DeleteTodo((int)p));
            changeDonStateCommand = new DelegateCommand(p => ChangeDoneState((int)p));
        }

        public void UpdateTodoList() {
            var todos = _api.GetTodos();
            ObservableTodoItems.Clear();
            foreach (var todo in todos) {
                ObservableTodoItems.Add(todo);
            }
        }

        public void AddTodo() {
            if (InputTitle == "") {
                return;
            }
            Todo todo = new Todo {
                Id = 0,
                Title = InputTitle,
                Description = "",
                Detline = DateTime.Now,
                HasDetline = false,
                IsCompleted = false
            };
            ObservableTodoItems.Add(_api.AddTodo(todo));
        }

        public void DeleteTodo(int id) {
            _api.DeleteTodo(id);
            var todoToRemove = ObservableTodoItems.FirstOrDefault(t => t.Id == id);
            if (todoToRemove != null) {
                ObservableTodoItems.Remove(todoToRemove);
            }
        }

        public void ChangeDoneState(int id) {
            var todo = ObservableTodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null) {
                return;
            }
            todo.IsCompleted = !todo.IsCompleted;
            _api.UpdateTodo(todo);
            UpdateTodoList();
        }
    }
}
