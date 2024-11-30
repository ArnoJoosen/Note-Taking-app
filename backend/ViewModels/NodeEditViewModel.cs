using Backend.Services;
using Shared.dto;

namespace Backend.ViewModels {
    public class NodeEditViewModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        IApiService _api;

        public NodeEditViewModel(IApiService api, int id) {
            NodeReadDto node = api.GetNodeById(id);
            Id = node.Id;
            Title = node.Title;
            Content = node.Content;
            _api = api;
        }

        public void Save() {
            NodeWriteDto node = new NodeWriteDto {
                Id = Id,
                Title = Title,
                Content = Content
            };
            _api.UpdateNode(node);
        }
    }
}
