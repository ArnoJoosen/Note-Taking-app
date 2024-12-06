using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shared.Models;
using Backend.Services;
using System.Windows.Input;
using Backend;
using Shared.dto;

namespace Backend.ViewModels {
    public class TodoViewModel {
        public event NotFountHandler NotFound;
        public event ConnectionErrorHandler ConnectionError;
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

        public async void UpdateTodoList() {
            try {
                List<TodoListItemReadDto> todos = await _api.GetTodosAsync();
                ObservableTodoItems.Clear();
                foreach (var todo in todos) {
                    ObservableTodoItems.Add(todo);
                }
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }

        public async void AddTodo() {
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
            try {
                ObservableTodoItems.Add(await _api.CreateTodoAsync(todo));
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }

        public void DeleteTodo(int id) {
            try {
                _api.DeleteTodoAsync(id);
                var todoToRemove = ObservableTodoItems.FirstOrDefault(t => t.Id == id);
                if (todoToRemove != null) {
                    ObservableTodoItems.Remove(todoToRemove);
                }
            } catch (NotFoundException) {
                NotFound?.Invoke(id);
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
            }
            
        }

        public void ChangeDoneState(int id) {
            bool done = ObservableTodoItems.First(t => t.Id == id).IsCompleted;
            try {
                _api.UpdateTodoStateAsync(id, !done);
                UpdateTodoList();
            } catch (NotFoundException) {
                NotFound?.Invoke(id);
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
            }
        }
    }
}
