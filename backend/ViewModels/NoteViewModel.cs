using Backend.Services;
using System.ComponentModel;

namespace Backend.ViewModels {
public class NoteViewModel : INotifyPropertyChanged {
    public event NotFountHandler NotFound;
    public event ConnectionErrorHandler ConnectionError;
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
        try {
            var node = await _api.GetNodeByIdAsync(id);
            Id = node.Id;
            Title = node.Title;
            Content = node.Content;
            CreatedAt = node.CreatedAt;
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
