using Shared.dto;

namespace Backend.Services {
    public interface IApiNoteService {
        public List<NodeListItemReadDto> GetNodes();
        public List<NodeListItemReadDto> GetFavoriteNodes();
        public NodeReadDto GetNodeById(int id);
        public NodeReadDto CreateNode(NodeWriteDto node);
        public void UpdateNode(NodeWriteDto node, int id);
        public void DeleteNode(int id);
        public void ChageNodeFavorite(int id, bool isFavorite);
    }
}
