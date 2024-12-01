using Backend.Services;
using Shared.dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Backend.ViewModels {
    public class NotesViewModel {
        public ObservableCollection<NoteListItemReadDto> ObservableNotes { get; set; } = new ObservableCollection<NoteListItemReadDto>();
        public String InputTitle { get; set; } = "";

        IApiNoteService _api;

        public ICommand addCommand { get; private set; }
        public ICommand deleteCommand { get; private set; }
        public ICommand ChangeNodeFavoriteCommand { get; private set; }

        public NotesViewModel(IApiNoteService api) {
            _api = api;
            addCommand = new DelegateCommand(p => AddNode());
            deleteCommand = new DelegateCommand(p => DeleteNode((int)p));
            ChangeNodeFavoriteCommand = new DelegateCommand(p => ChangeNodeFavorite((int)p));
        }

        public async void UpdateNodeList() {
            ObservableNotes.Clear();
            foreach (var node in await _api.GetNodesAsync()) {
                ObservableNotes.Add(node);
            }
        }

        public void AddNode() {
            NoteWriteDto node = new NoteWriteDto { Title = InputTitle, Content = "" };
            _api.CreateNodeAsync(node);
            UpdateNodeList();
        }

        public void DeleteNode(int id) {
            _api.DeleteNodeAsync(id);
            var nodeToRemove = ObservableNotes.FirstOrDefault(n => n.Id == id);
            if (nodeToRemove != null) {
                ObservableNotes.Remove(nodeToRemove);
            }
        }

        public void ChangeNodeFavorite(int id) {
            bool faborite = ObservableNotes.FirstOrDefault(n => n.Id == id).IsFavorite;
            _api.ChageNodeFavoriteAsync(id, !faborite);
            UpdateNodeList();
        }
    }
}
