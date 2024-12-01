using Shared.dto;
using Shared.Models;

namespace Backend.Services {
    public class MockApiNoteService : IApiNoteService {
        int currentNodeId = 0;
        List<Node> _nodes = new();

        public MockApiNoteService() {
            _nodes.Add(new Node { Id = currentNodeId++, Content = "This is the first node", Title = "First node", CreatedAt = DateTime.Now, IsFavorite = true });
            _nodes.Add(new Node { Id = currentNodeId++, Content = "This is the second node", Title = "Second node", CreatedAt = DateTime.Now, IsFavorite = false });
            _nodes.Add(new Node { Id = currentNodeId++, Content = "This is the third node", Title = "Third node", CreatedAt = DateTime.Now, IsFavorite = false });
        }


        // -------------------- Node API --------------------
        public List<NodeListItemReadDto> GetNodes() {
            List<NodeListItemReadDto> nodesList = new();
            foreach (var node in _nodes) {
                nodesList.Add(new NodeListItemReadDto {
                    Id = node.Id,
                    Title = node.Title,
                    CreatedAt = node.CreatedAt,
                    IsFavorite = node.IsFavorite
                });
            }
            return nodesList;
        }

        public List<NodeListItemReadDto> GetFavoriteNodes() {
            List<NodeListItemReadDto> nodesList = new();
            _nodes.Where(n => n.IsFavorite == true).ToList().ForEach(node => {
                nodesList.Add(new NodeListItemReadDto {
                    Id = node.Id,
                    Title = node.Title,
                    CreatedAt = node.CreatedAt,
                    IsFavorite = node.IsFavorite
                });
            });
            return nodesList;
        }

        public NodeReadDto GetNodeById(int id) {
            Node? node = _nodes.Where(n => n.Id == id).First();
            if (node == null) {
                throw new Exception("Node not found");
            }
            return new NodeReadDto {
                Id = node.Id,
                Title = node.Title,
                Content = node.Content,
                CreatedAt = node.CreatedAt
            };
        }

        public void UpdateNode(NodeWriteDto node, int id) {
            if (node == null) {
                throw new Exception("Node not found");
            }

            Node? existingNode = _nodes.Find(n => n.Id == id);
            if (existingNode == null) {
                throw new Exception("Node not found");
            }

            existingNode.Title = node.Title;
            existingNode.Content = node.Content;
        }

        public void DeleteNode(int id) {
            Node? node = _nodes.Find(n => n.Id == id);
            if (node == null) {
                throw new Exception("Node not found");
            }
            _nodes.Remove(node);
        }
        public NodeReadDto CreateNode(NodeWriteDto node) {
            Node nodenew = new Node {
                Id = currentNodeId++,
                Title = node.Title,
                Content = node.Content,
                CreatedAt = DateTime.Now
            };
            _nodes.Add(nodenew);
            return new NodeReadDto {
                Id = nodenew.Id,
                Title = nodenew.Title,
                Content = nodenew.Content,
                CreatedAt = nodenew.CreatedAt
            };
        }

        public void ChageNodeFavorite(int id, bool isFavorite) {
            Node? node = _nodes.Find(n => n.Id == id);
            if (node == null) {
                throw new Exception("Node not found");
            }
            node.IsFavorite = isFavorite;
        }
    }
}
