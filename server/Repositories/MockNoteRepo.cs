using Shared.Models;

namespace Server.Repositories {
    public class MockNoteRepo : INoTeRepo {
        int _curentId = 0;

        List<Note> nodes = new();

        public MockNoteRepo() {
            nodes.Add(new Note { Id = _curentId++, Title = "First node", Content = "This is the first node", CreatedAt = DateTime.Now, IsFavorite = true });
            nodes.Add(new Note { Id = _curentId++, Title = "Second node", Content = "This is the second node", CreatedAt = DateTime.Now, IsFavorite = false });
            nodes.Add(new Note { Id = _curentId++, Title = "Third node", Content = "This is the third node", CreatedAt = DateTime.Now, IsFavorite = false });
        }

        public IEnumerable<Note> GetAllNodes() {
            return nodes;
        }

        public IEnumerable<Note> GetFavoriteNodes() {
            return nodes.Where(node => node.IsFavorite);
        }
        public Note GetNodeById(int id) {
            var node = nodes.FirstOrDefault(node => node.Id == id);
            if (node == null) {
                throw new KeyNotFoundException($"Node with Id {id} not found.");
            }
            return node;
        }
        public Note CreateNode(string Title, string Content) {
            Note node = new Note {
                Id = _curentId++,
                Title = Title,
                Content = Content,
                CreatedAt = DateTime.Now,
                IsFavorite = false
            };
            nodes.Add(node);
            return node;
        }

        public void UpdateNodeById(Note node) {
            if (node == null) {
                throw new ArgumentNullException(nameof(node));
            }
            Note? oNode = nodes.FirstOrDefault(n => n.Id == node.Id);
            if (node != null) {
                oNode.Title = node.Title;
                oNode.Content = node.Content;
            } else {
                throw new KeyNotFoundException($"Node with Id {node.Id} not found.");
            }
        }
        public void ChangeNodeFavorite(int id, bool isFavorite) {
            var node = nodes.FirstOrDefault(node => node.Id == id);
            if (node != null) {
                node.IsFavorite = isFavorite;
            } else {
                throw new KeyNotFoundException($"Node with Id {id} not found.");
            }
        }
        public void DeleteNodeById(int id) {
            var node = nodes.FirstOrDefault(node => node.Id == id);
            if (node != null) {
                nodes.Remove(node);
            } else {
                throw new KeyNotFoundException($"Node with Id {id} not found.");
            }
        }
    }
}
