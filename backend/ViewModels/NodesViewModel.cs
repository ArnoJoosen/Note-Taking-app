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
    public class NodesViewModel {
        public ObservableCollection<NodeListItemReadDto> ObservableNotes { get; set; } = new ObservableCollection<NodeListItemReadDto>();
        public String InputTitle { get; set; } = "";

        IApiService _api;

        public ICommand addCommand { get; private set; }
        public ICommand deleteCommand { get; private set; }
        public ICommand ChangeNodeFavoriteCommand { get; private set; }

        public NodesViewModel(IApiService api) {
            _api = api;
            UpdateNodeList();
            addCommand = new DelegateCommand(p => AddNode());
            deleteCommand = new DelegateCommand(p => DeleteNode((int)p));
            ChangeNodeFavoriteCommand = new DelegateCommand(p => ChangeNodeFavorite((int)p));
        }

        public void UpdateNodeList() {
            ObservableNotes.Clear();
            foreach (var node in _api.GetNodes()) {
                ObservableNotes.Add(node);
            }
        }

        public void AddNode() {
            NodeWriteDto node = new NodeWriteDto { Title = InputTitle };
            _api.AddNode(node);
            UpdateNodeList();
        }

        public void DeleteNode(int id) {
            _api.DeleteNode(id);
            var nodeToRemove = ObservableNotes.FirstOrDefault(n => n.Id == id);
            if (nodeToRemove != null) {
                ObservableNotes.Remove(nodeToRemove);
            }
        }

        public void ChangeNodeFavorite(int id) {
            bool faborite = ObservableNotes.FirstOrDefault(n => n.Id == id).IsFavorite;
            _api.ChageNodeFavorite(id, !faborite);
            UpdateNodeList();
        }
    }
}
