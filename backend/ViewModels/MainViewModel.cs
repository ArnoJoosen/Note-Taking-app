using Backend.Services;
using Shared.dto;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Backend.ViewModels {
    public class MainViewModel {
        public ObservableCollection<NoteListItemReadDto> FavoritesNods { get; set; } = new ObservableCollection<NoteListItemReadDto>();
        public ObservableCollection<TodoListItemReadDto> NotCompletedTodos { get; set; } = new ObservableCollection<TodoListItemReadDto>();
        public ICommand CompleteTodoCommand { get; private set; }

        IApiTodoService _apiTodo;
        IApiNoteService _apiNode;

        public MainViewModel(IApiTodoService apiTodo, IApiNoteService apiNode) {
            _apiTodo = apiTodo;
            _apiNode = apiNode;
            UpdateNodeFavorites();
            UpdateTodoNotDone();
            CompleteTodoCommand = new DelegateCommand(p => CompleteTodoItem((int)p));
        }

        public async void UpdateNodeFavorites() {
            FavoritesNods.Clear();
            var nodes = await _apiNode.GetFavoriteNodesAsync();
            nodes.ForEach(n => FavoritesNods.Add(n));
        }

        public async void UpdateTodoNotDone() {
            NotCompletedTodos.Clear();
            var nodes = _apiTodo.GetNotCompletedTodos();
             nodes.ForEach(t => NotCompletedTodos.Add(t));
        }

        public void CompleteTodoItem(int id) {
            _apiTodo.UpdateTodoState(id, true);
            var todo = NotCompletedTodos.FirstOrDefault(t => t.Id == id);
            if (todo != null) {
                NotCompletedTodos.Remove(todo);
            }
        }
    }
}
