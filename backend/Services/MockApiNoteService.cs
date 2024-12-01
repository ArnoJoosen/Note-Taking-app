using Shared.dto;
using Shared.Models;

namespace Backend.Services {
    public class MockApiNoteService : IApiNoteService {
        int currentNodeId = 0;
        List<Note> _nodes = new();

        public MockApiNoteService() {
            _nodes.Add(new Note { Id = currentNodeId++, Content = "This is the first node", Title = "First node", CreatedAt = DateTime.Now, IsFavorite = true });
            _nodes.Add(new Note { Id = currentNodeId++, Content = "This is the second node", Title = "Second node", CreatedAt = DateTime.Now, IsFavorite = false });
            _nodes.Add(new Note { Id = currentNodeId++, Content = "This is the third node", Title = "Third node", CreatedAt = DateTime.Now, IsFavorite = false });
        }


        // -------------------- Node API --------------------
        public List<NoteListItemReadDto> GetNodes() {
            List<NoteListItemReadDto> nodesList = new();
            foreach (var node in _nodes) {
                nodesList.Add(new NoteListItemReadDto {
                    Id = node.Id,
                    Title = node.Title,
                    CreatedAt = node.CreatedAt,
                    IsFavorite = node.IsFavorite
                });
            }
            return nodesList;
        }

        public List<NoteListItemReadDto> GetFavoriteNodes() {
            List<NoteListItemReadDto> nodesList = new();
            _nodes.Where(n => n.IsFavorite == true).ToList().ForEach(node => {
                nodesList.Add(new NoteListItemReadDto {
                    Id = node.Id,
                    Title = node.Title,
                    CreatedAt = node.CreatedAt,
                    IsFavorite = node.IsFavorite
                });
            });
            return nodesList;
        }

        public NoteReadDto GetNodeById(int id) {
            Note? node = _nodes.Where(n => n.Id == id).First();
            if (node == null) {
                throw new Exception("Node not found");
            }
            return new NoteReadDto {
                Id = node.Id,
                Title = node.Title,
                Content = node.Content,
                CreatedAt = node.CreatedAt
            };
        }

        public void UpdateNode(NoteWriteDto node, int id) {
            if (node == null) {
                throw new Exception("Node not found");
            }

            Note? existingNode = _nodes.Find(n => n.Id == id);
            if (existingNode == null) {
                throw new Exception("Node not found");
            }

            existingNode.Title = node.Title;
            existingNode.Content = node.Content;
        }

        public void DeleteNode(int id) {
            Note? node = _nodes.Find(n => n.Id == id);
            if (node == null) {
                throw new Exception("Node not found");
            }
            _nodes.Remove(node);
        }
        public NoteReadDto CreateNode(NoteWriteDto node) {
            Note nodenew = new Note {
                Id = currentNodeId++,
                Title = node.Title,
                Content = node.Content,
                CreatedAt = DateTime.Now
            };
            _nodes.Add(nodenew);
            return new NoteReadDto {
                Id = nodenew.Id,
                Title = nodenew.Title,
                Content = nodenew.Content,
                CreatedAt = nodenew.CreatedAt
            };
        }

        public void ChageNodeFavorite(int id, bool isFavorite) {
            Note? node = _nodes.Find(n => n.Id == id);
            if (node == null) {
                throw new Exception("Node not found");
            }
            node.IsFavorite = isFavorite;
        }
    }
}
