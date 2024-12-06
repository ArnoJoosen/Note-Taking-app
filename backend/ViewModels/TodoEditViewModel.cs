using System.ComponentModel;
using Backend.Services;
using Shared.dto;

namespace Backend.ViewModels {
    public class TodoEditViewModel : INotifyPropertyChanged {
        public event NotFountHandler NotFound;
        public event ConnectionErrorHandler ConnectionError;

        IApiTodoService _api;
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

        public TodoEditViewModel(IApiTodoService api) {
            _api = api;
        }

        public async Task Load(int id) {
            ItemId = id;
            try {
                TodoReadDto todo = await _api.GetTodoByIdAsync(id);
                Title = todo.Title;
                Description = todo.Description;
                HasDetline = todo.HasDetline;
                IsCompleted = todo.IsCompleted;
            } catch (NotFoundException) {
                NotFound?.Invoke(id);
                return;
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }

        public void Save() {
            try {
                _api.UpdateTodoAsync(new TodoWriteDto {
                    Title = Title,
                    Description = Description,
                    HasDetline = HasDetline,
                    Detline = DetLine,
                    IsCompleted = IsCompleted
                }, ItemId);
            } catch (NotFoundException) {
                NotFound?.Invoke(ItemId);
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
            }
        }
    }
}
