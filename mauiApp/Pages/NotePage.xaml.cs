using Backend.Services;
using Backend.ViewModels;
using Shared.Models;

namespace mauiApp.Pages;

[QueryProperty(nameof(NodeId), "id")]
public partial class NotePage : ContentPage {
    private int _Id;
    NoteViewModel _vm;
    IApiNoteService _api;
    public int NodeId {
        get => _Id;
        set {
            _Id = value;
            LoadNote();
        }
    }

    public NotePage(IApiNoteService api) {
        _api = api;
        InitializeComponent();
    }

    private void LoadNote() {
        _vm = new(_api, _Id);
        BindingContext = _vm;
    }

    public async void OnEditClicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync($"NoteEditPage?id={_Id}");
    }
}
