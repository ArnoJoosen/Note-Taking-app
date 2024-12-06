namespace mauiApp.Pages;

using Backend.ViewModels;

public partial class MainPage : ContentPage {
    int count = 0;
    MainViewModel _vm;

    public MainPage(MainViewModel vm, HttpClient httpClient) {
        InitializeComponent();
        _vm = vm;
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
    public void OnConnectionError() {
        DisplayAlert("Error", "Connection error", "Ok");
    }
}
