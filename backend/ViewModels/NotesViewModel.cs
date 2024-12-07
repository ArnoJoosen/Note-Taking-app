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
        public event NotFountHandler NotFound;
        public event ConnectionErrorHandler ConnectionError;
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
            try {
                foreach (var node in await _api.GetNodesAsync()) {
                    ObservableNotes.Add(node);
                }
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }

        public async void AddNode() {
            NoteWriteDto node = new NoteWriteDto { Title = InputTitle, Content = "" };
            try {
                await _api.CreateNodeAsync(node);
                UpdateNodeList();
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }

        public void DeleteNode(int id) {
            try {
                _api.DeleteNodeAsync(id);
                var nodeToRemove = ObservableNotes.FirstOrDefault(n => n.Id == id);
                if (nodeToRemove != null) {
                    ObservableNotes.Remove(nodeToRemove);
                }
            } catch (NotFoundException) {
                NotFound?.Invoke(id);
                return;
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }

        public void ChangeNodeFavorite(int id) {
            bool faborite = ObservableNotes.FirstOrDefault(n => n.Id == id).IsFavorite;
            try {
                _api.ChageNodeFavoriteAsync(id, !faborite);
                UpdateNodeList();
            } catch (NotFoundException) {
                NotFound?.Invoke(id);
                return;
            } catch (ConnectionErrorException) {
                ConnectionError?.Invoke();
                return;
            }
        }
    }
}
