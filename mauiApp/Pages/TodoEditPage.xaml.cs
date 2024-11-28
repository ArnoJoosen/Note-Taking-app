namespace mauiApp.Pages;
using Shared.Models;
using Backend.ViewModels;
using Backend.Services;

[QueryProperty(nameof(TodoId), "id")]
public partial class TodoEditPage : ContentPage
{
    private int _id;
    ITodoApiServer _api;
    TodoEditViewModel _vm;

    public string TodoId {
        set {
            if (int.TryParse(value, out int id)) {
                _id = id;
                LoadTodo();
            }
        }
    }

    private void LoadTodo() {
        _vm = new TodoEditViewModel(_api, _id);
        BindingContext = _vm;
    }
    public TodoEditPage(ITodoApiServer api) {
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
