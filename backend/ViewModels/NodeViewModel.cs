using Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ViewModels {
    public class NodeViewModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public NodeViewModel(IApiNoteService api, int id) {
            var node = api.GetNodeById(id);
            Id = node.Id;
            Title = node.Title;
            Content = node.Content;
            CreatedAt = node.CreatedAt;
        }
    }
}
