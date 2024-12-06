using Backend.Services;
using Backend.ViewModels;
using Shared.Models;

namespace mauiApp.Pages;

[QueryProperty(nameof(NodeId), "id")]
public partial class NotePage : ContentPage {
    private int _Id = -1;
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
        _vm = new(_api);
        _vm.NotFound += OnNotFound;
        _vm.ConnectionError += OnConnectionError;
        InitializeComponent();
    }

    private async Task LoadNote() {
        await _vm.LoadNote(_Id);
        BindingContext = _vm;
        ToolbarItems[0].SetValue(MenuItem.IsEnabledProperty, true);
        Indicator.IsVisible = false;
    }

    public async void OnEditClicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync($"NoteEditPage?id={_Id}");
    }

    protected async override void OnAppearing() {
        if (_Id != -1) await _vm.LoadNote(_Id);
    }

    public void OnConnectionError() {
        Navigation.PopToRootAsync(); // go back to the main page if there is a connection error
    }
    public void OnNotFound(int id) {
        Navigation.PopAsync(); // go back to the previous page if the note is not found
    }
}
