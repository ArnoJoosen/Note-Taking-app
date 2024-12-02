namespace mauiApp.Pages;
using Shared.Models;
using Backend.ViewModels;
using Backend.Services;

[QueryProperty(nameof(TodoId), "id")]
public partial class TodoEditPage : ContentPage
{
    private int _Id;
    TodoEditViewModel _vm;
    IApiTodoService _api;

    public int TodoId {
        get => _Id;
        set {
            _Id = value;
            LoadTodo();
        }
    }

    private async Task LoadTodo() {
        _vm = new TodoEditViewModel(_api);
        await _vm.Load(_Id);
        BindingContext = _vm;
        TitleEditor.IsEnabled = true;
        DescriptionEditor.IsEnabled = true;
        HasDeadlineCheckbox.IsEnabled = true;
        DeadlinePicker.IsEnabled = true;
        CompletedCheckbox.IsEnabled = true;
        SaveButton.IsEnabled = true;
    }

    public TodoEditPage(IApiTodoService api) {
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
