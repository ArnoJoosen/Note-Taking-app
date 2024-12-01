using Backend.Services;
using Shared.dto;

namespace Backend.ViewModels {
    public class NoteEditViewModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        IApiNoteService _api;

        public NoteEditViewModel(IApiNoteService api, int id) {
            NoteReadDto node = api.GetNodeById(id);
            Id = node.Id;
            Title = node.Title;
            Content = node.Content;
            _api = api;
        }

        public void Save() {
            NoteWriteDto node = new NoteWriteDto {
                Title = Title,
                Content = Content
            };
            _api.UpdateNode(node, Id);
        }
    }
}
