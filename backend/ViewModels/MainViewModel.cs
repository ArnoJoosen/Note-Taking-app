using Backend.Services;
using Shared.dto;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Backend.ViewModels {
    public class MainViewModel {
        public event NotFountHandler NotFound;
        public event ConnectionErrorHandler ConnectionError;
        public ObservableCollection<NoteListItemReadDto> FavoritesNods { get; set; } = new ObservableCollection<NoteListItemReadDto>();
        public ObservableCollection<TodoListItemReadDto> NotCompletedTodos { get; set; } = new ObservableCollection<TodoListItemReadDto>();
        public ICommand CompleteTodoCommand { get; private set; }

        IApiTodoService _apiTodo;
        IApiNoteService _apiNode;

        public MainViewModel(IApiTodoService apiTodo, IApiNoteService apiNode) {
            _apiTodo = apiTodo;
            _apiNode = apiNode;
            CompleteTodoCommand = new DelegateCommand(p => CompleteTodoItem((int)p));
        }

        public async void UpdateNodeFavorites() {
            FavoritesNods.Clear();
            try {
                var nodes = await _apiNode.GetFavoriteNodesAsync();
                nodes.ForEach(n => FavoritesNods.Add(n));
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }

        public async void UpdateTodoNotDone() {
            NotCompletedTodos.Clear();
            try {
                var nodes = await _apiTodo.GetNotCompletedTodosAsync();
                nodes.ForEach(t => NotCompletedTodos.Add(t));
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }

        public void CompleteTodoItem(int id) {
            try {
                _apiTodo.UpdateTodoStateAsync(id, true);
                var todo = NotCompletedTodos.FirstOrDefault(t => t.Id == id);
                if (todo != null) {
                    NotCompletedTodos.Remove(todo);
                }
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }
    }
}
