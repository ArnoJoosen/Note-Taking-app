using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shared.Models;
using Backend.Services;
using System.Windows.Input;
using Backend;
using Shared.dto;

namespace Backend.ViewModels
{
    public class TodoViewModel {
        public ObservableCollection<TodoListItemReadDto> ObservableTodoItems { get; set; } = new();
        public String InputTitle { get; set; } = "";

        private IApiTodoService _api;

        public ICommand addCommand { get; private set; }
        public ICommand deleteCommand { get; private set; }
        public ICommand changeDonStateCommand { get; private set; }

        public TodoViewModel(IApiTodoService api) {
            _api = api;
            addCommand = new DelegateCommand(p => AddTodo());
            deleteCommand = new DelegateCommand(p => DeleteTodo((int)p));
            changeDonStateCommand = new DelegateCommand(p => ChangeDoneState((int)p));
        }

        public void UpdateTodoList() {
            List<TodoListItemReadDto> todos = _api.GetTodos();
            ObservableTodoItems.Clear();
            foreach (var todo in todos) {
                ObservableTodoItems.Add(todo);
            }
        }

        public void AddTodo() {
            if (InputTitle == "") {
                return;
            }
            TodoWriteDto todo = new TodoWriteDto {
                Title = InputTitle,
                Description = "",
                Detline = DateTime.Now,
                HasDetline = false,
                IsCompleted = false
            };
            ObservableTodoItems.Add(_api.CreateTodo(todo));
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
            _api.UpdateTodoState(id, todo.IsCompleted);
            UpdateTodoList();
        }
    }
}
