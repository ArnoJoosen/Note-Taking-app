using Shared.dto;

namespace Backend.Services {
    public interface IApiNoteService {
        public string BaseAddress { get; set; }
        public Task<List<NoteListItemReadDto>> GetNodesAsync();
        public Task<List<NoteListItemReadDto>> GetFavoriteNodesAsync();
        public Task<NoteReadDto> GetNodeByIdAsync(int id);
        public NoteReadDto GetNodeById(int id);
        public Task<NoteReadDto> CreateNodeAsync(NoteWriteDto node);
        public Task UpdateNodeAsync(NoteWriteDto node, int id);
        public Task DeleteNodeAsync(int id);
        public Task ChageNodeFavoriteAsync(int id, bool isFavorite);
    }
}
