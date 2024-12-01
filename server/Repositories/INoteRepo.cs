using Shared.Models;

namespace Server.Repositories {
    public interface INoTeRepo {
        public IEnumerable<Note> GetAllNodes();
        public IEnumerable<Note> GetFavoriteNodes();
        public Note GetNodeById(int id);
        public Note CreateNode(string Title, string Content);
        public void UpdateNodeById(Note id);
        public void ChangeNodeFavorite(int id, bool isFavorite);
        public void DeleteNodeById(int id);
    }
}
