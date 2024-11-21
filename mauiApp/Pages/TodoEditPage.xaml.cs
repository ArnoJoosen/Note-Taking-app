namespace mauiApp.Pages;
using Shared.Models;
using Backend.ViewModels;

public partial class TodoEditPage : ContentPage
{
    TodoEditViewModel _vm;
    public TodoEditPage(Todo todo)
    {
        InitializeComponent();
        _vm = new TodoEditViewModel(todo);
        BindingContext = _vm;
    }

    protected override bool OnBackButtonPressed()
    {
        Navigation.PopAsync();
        return true;
    }
}
