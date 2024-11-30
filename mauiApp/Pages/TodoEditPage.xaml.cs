namespace mauiApp.Pages;
using Shared.Models;
using Backend.ViewModels;
using Backend.Services;

[QueryProperty(nameof(TodoId), "id")]
public partial class TodoEditPage : ContentPage
{
    private int _Id;
    TodoEditViewModel _vm;
    IApiService _api;

    public int TodoId {
        get => _Id;
        set {
            _Id = value;
            LoadTodo();
        }
    }

    private void LoadTodo() {
        _vm = new TodoEditViewModel(_api, _Id);
        BindingContext = _vm;
    }

    public TodoEditPage(IApiService api) {
        _api = api;
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed() {
        Navigation.PopAsync();
        return true;
    }

    public void OnClickSave(object sender, EventArgs e) {
        if (_vm != null) {
            _vm.Save();
        }
        Navigation.PopAsync();
    }
}
