using Backend.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.ViewModels {
public class NoteViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;

    public int Id { get; set; }

    private string _title;
    public string Title {
        get => _title;
        set {
            _title = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
        }
    }

    private string _content;
    public string Content {
        get => _content;
        set {
            _content = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Content)));
        }
    }

    public DateTime CreatedAt { get; set; }

    IApiNoteService _api;

    public NoteViewModel(IApiNoteService api) {
        _api = api;
    }

    public async Task LoadNote(int id) {
        var node = await _api.GetNodeByIdAsync(id);
        Id = node.Id;
        Title = node.Title;
        Content = node.Content;
        CreatedAt = node.CreatedAt;
    }
}
}
