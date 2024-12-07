namespace mauiApp.Pages;

using Backend.Services;
using Backend.ViewModels;

public partial class MainPage : ContentPage {
    int count = 0;
    MainViewModel _vm;
    IApiNoteService _apiNote;
    IApiTodoService _apiTodo;

    public MainPage(MainViewModel vm, IApiNoteService apiNote, IApiTodoService apiTodo) {
        InitializeComponent();
        _vm = vm;
        _apiNote = apiNote;
        _apiTodo = apiTodo;
        BindingContext = vm;
        _vm.ConnectionError += OnConnectionError;
    }

    protected override void OnAppearing() {
        _vm.UpdateNodeFavorites();
        _vm.UpdateTodoNotDone();
    }

    public async void OnNoteTapped(object sender, TappedEventArgs e) {
        if (e.Parameter is int tappedItem) {
            await Shell.Current.GoToAsync($"NotePage?id={tappedItem}");
        }
    }
    public async void OnConnectionError() {
        string result = await DisplayPromptAsync(
            "Connection Error",
            "Please enter new base URL:",
            initialValue: _apiNote.BaseAddress,
            maxLength: 100,
            keyboard: Keyboard.Url
        );
        if (result != null) { // User entered a URL
            _apiNote.BaseAddress = result;
            _apiTodo.BaseAddress = result;
            _vm.UpdateNodeFavorites();
            _vm.UpdateTodoNotDone();
        } else { // User cancelled
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }
}
