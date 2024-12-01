using Backend.Services;
using Shared.dto;

namespace Backend.ViewModels {
    public class NoteEditViewModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        IApiNoteService _api;

        public NoteEditViewModel(IApiNoteService api) {
            _api = api;
        }

        public async Task LoadNote(int id) {
            var node = await _api.GetNodeByIdAsync(id);
            Id = node.Id;
            Title = node.Title;
            Content = node.Content;
        }

        public void Save() {
            NoteWriteDto node = new NoteWriteDto {
                Title = Title,
                Content = Content
            };
            _api.UpdateNodeAsync(node, Id);
        }
    }
}
