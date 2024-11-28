using Backend.ViewModels;
using Shared.Models;
namespace mauiApp.Pages;

public partial class TodoPage : ContentPage {

    TodoViewModel _vm;
    public TodoPage(TodoViewModel vm) {
        _vm = vm;
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
}
