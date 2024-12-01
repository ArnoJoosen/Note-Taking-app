using Shared.dto;

namespace Backend.Services {
    public interface IApiNoteService {
        public List<NoteListItemReadDto> GetNodes();
        public List<NoteListItemReadDto> GetFavoriteNodes();
        public NoteReadDto GetNodeById(int id);
        public NoteReadDto CreateNode(NoteWriteDto node);
        public void UpdateNode(NoteWriteDto node, int id);
        public void DeleteNode(int id);
        public void ChageNodeFavorite(int id, bool isFavorite);
    }
}
