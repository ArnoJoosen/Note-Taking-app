using Server.Contexts;
using Server.Repositories;
using Shared.Models;

namespace Server.Repositories {
    public class NoteRepo : INoTeRepo {

        private readonly SQLContext _context;
        public NoteRepo(SQLContext context) {
            _context = context;
        }
        public IEnumerable<Note> GetAllNodes() {
            return _context.Notes.ToList();
        }
        public IEnumerable<Note> GetFavoriteNodes() {
            return _context.Notes.Where(n => n.IsFavorite == true).ToList();
        }
        public Note GetNodeById(int id) {
            var item = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (item != null) {
                return item;
            }
            throw new System.Exception("Note not found");
        }
        public Note CreateNode(string Title, string Content) {
            var newNote = new Note {
                Title = Title,
                Content = Content,
                IsFavorite = false,
                CreatedAt = DateTime.Now
            };
            _context.Notes.Add(newNote);
            _context.SaveChanges();
            return newNote;
        }
        public void UpdateNodeById(Note id) {
            var item = _context.Notes.FirstOrDefault(n => n.Id == id.Id);
            if (item != null) {
                item.Title = id.Title;
                item.Content = id.Content;
                _context.SaveChanges();
                return;
            }
            throw new System.Exception("Note not found");
        }
        public void ChangeNodeFavorite(int id, bool isFavorite) {
            var item = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (item != null) {
                item.IsFavorite = isFavorite;
                _context.SaveChanges();
                return;
            }
            throw new System.Exception("Note not found");
        }
        public void DeleteNodeById(int id) {
            var item = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (item != null) {
                _context.Notes.Remove(item);
                _context.SaveChanges();
                return;
            }
            throw new System.Exception("Note not found");
        }
    }
}
