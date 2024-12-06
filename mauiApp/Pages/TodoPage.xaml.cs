using Backend.ViewModels;
using Shared.Models;
namespace mauiApp.Pages;

public partial class TodoPage : ContentPage {

    TodoViewModel _vm;
    public TodoPage(TodoViewModel vm) {
        _vm = vm;
        _vm.NotFound += OnNotFound;
        _vm.ConnectionError += OnConnectionError;
        BindingContext = vm;
        InitializeComponent();
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        _vm.UpdateTodoList();
    }
    public async void OnItemTapped(object sender, TappedEventArgs e) {
        if (e.Parameter is int tappedItem) {
            await Shell.Current.GoToAsync($"TodoEditPage?id={tappedItem}");
        }
    }

    public void OnConnectionError() {
        Navigation.PopToRootAsync(); // go back to the main page if there is a connection error
    }
    public void OnNotFound(int id) {
        DisplayAlert("Todo not found", $"Todo with id {id} was not found", "OK");
        _vm.UpdateTodoList();
    }
}
