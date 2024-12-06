namespace mauiApp.Pages;
using Backend.ViewModels;
using Backend.Services;

[QueryProperty(nameof(TodoId), "id")]
public partial class TodoEditPage : ContentPage
{
    private int _Id;
    TodoEditViewModel _vm;

    public int TodoId {
        get => _Id;
        set {
            _Id = value;
            _ = LoadTodo(); // LoadTodo is an async method so we need to wait for it to finish
        }
    }

    private async Task LoadTodo() {
        await _vm.Load(_Id);
        BindingContext = _vm;
        Indicator.IsVisible = false;
        TitleEditor.IsEnabled = true;
        DescriptionEditor.IsEnabled = true;
        HasDeadlineCheckbox.IsEnabled = true;
        DeadlinePicker.IsEnabled = true;
        CompletedCheckbox.IsEnabled = true;
        SaveButton.IsEnabled = true;
    }

    public TodoEditPage(IApiTodoService api) {
        _vm = new TodoEditViewModel(api);
        _vm.NotFound += OnNotFound;
        _vm.ConnectionError += OnConnectionError;
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

    public void OnConnectionError() {
        Navigation.PopToRootAsync(); // go back to the main page if there is a connection error
    }
    public void OnNotFound(int id) {
        Navigation.PopAsync(); // go back to the previous page if the note is not found
    }
}
