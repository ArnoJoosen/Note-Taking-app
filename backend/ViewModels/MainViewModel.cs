using Backend.Services;
using Shared.dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Backend.ViewModels {
    public class MainViewModel {
        public ObservableCollection<NodeListItemReadDto> FavoritesNods { get; set; } = new ObservableCollection<NodeListItemReadDto>();
        public ObservableCollection<TodoListItemReadDto> NotCompletedTodos { get; set; } = new ObservableCollection<TodoListItemReadDto>();
        public ICommand CompleteTodoCommand { get; private set; }

        IApiService _api;

        public MainViewModel(IApiService api) {
            _api = api;
            UpdateNodeFavorites();
            UpdateTodoNotDone();
            CompleteTodoCommand = new DelegateCommand(p => CompleteTodoItem((int)p));
        }

        public void UpdateNodeFavorites() {
            _api.GetFavoriteNodes().ForEach(n => FavoritesNods.Add(n));
        }

        public void UpdateTodoNotDone() {
            _api.GetNotCompletedTodos().ForEach(t => NotCompletedTodos.Add(t));
        }

        public void CompleteTodoItem(int id) {
            _api.UpdateTodoState(id, true);
            var todo = NotCompletedTodos.FirstOrDefault(t => t.Id == id);
            if (todo != null) {
                NotCompletedTodos.Remove(todo);
            }
        }
    }
}
