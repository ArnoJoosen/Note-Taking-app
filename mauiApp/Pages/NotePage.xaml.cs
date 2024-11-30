using Backend.Services;
using Backend.ViewModels;
using Shared.Models;

namespace mauiApp.Pages;

[QueryProperty(nameof(NodeId), "id")]
public partial class NotePage : ContentPage {
    private int _Id;
    NodeViewModel _vm;
    IApiService _api;
    public int NodeId {
        get => _Id;
        set {
            _Id = value;
            LoadNode();
        }
    }

    public NotePage(IApiService api) {
        _api = api;
        InitializeComponent();
    }

    private void LoadNode() {
        _vm = new NodeViewModel(_api, _Id);
        BindingContext = _vm;
    }

    public async void OnEditClicked(object sender, EventArgs e) {
        //await Shell.Current.GoToAsync($"NoteEditPage?id={_Id}");
    }
}