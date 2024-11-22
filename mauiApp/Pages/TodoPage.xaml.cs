using Backend.ViewModels;
namespace mauiApp.Pages;

public partial class TodoPage : ContentPage
{

    TodoViewModel _vm;
    public TodoPage(TodoViewModel vm)
    {
        _vm = vm;
        BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        _vm.UpdateTodoList();
    }
    public async void OnItemTapped(object sender, TappedEventArgs e) {
        
    }
}
