using Backend.Services;
using Shared.dto;

namespace Backend.ViewModels {
    public class NoteEditViewModel {
        public event NotFountHandler NotFound;
        public event ConnectionErrorHandler ConnectionError;
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        IApiNoteService _api;

        public NoteEditViewModel(IApiNoteService api) {
            _api = api;
        }

        public async Task LoadNote(int id) {
            try {
                var node = await _api.GetNodeByIdAsync(id);
                Id = node.Id;
                Title = node.Title;
                Content = node.Content;
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }

        public Task Save() {
            NoteWriteDto node = new NoteWriteDto {
                Title = Title,
                Content = Content
            };
            try {
                return _api.UpdateNodeAsync(node, Id);
            } catch (NotFoundException) {
                NotFound?.Invoke(Id);
                return Task.CompletedTask;
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return Task.CompletedTask;
            }
        }
    }
}
