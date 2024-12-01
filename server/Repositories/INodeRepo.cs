using Shared.Models;

namespace Server.Repositories {
    public interface INodeRepo {
        public IEnumerable<Node> GetAllNodes();
        public IEnumerable<Node> GetFavoriteNodes();
        public Node GetNodeById(int id);
        public Node CreateNode(string Title, string Content);
        public void UpdateNodeById(Node id);
        public void ChangeNodeFavorite(int id, bool isFavorite);
        public void DeleteNodeById(int id);
    }
}
