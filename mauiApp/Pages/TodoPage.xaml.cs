using Backend.ViewModels;
using Shared.Models;
using System.Diagnostics;

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

    private void Add_Button_Clicked(object sender, EventArgs e)
    {
        _vm.addTodo();
    }

    private void Delete_Invoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var todo = swipeItem.BindingContext as Todo;

        if (todo != null)
        {
            _vm.removeTodo(todo.Id);
            _vm.ObservableTodoItems.Remove(todo);
        }
    }

    public void OnItemTapped(object sender, EventArgs e)
    {
        var tappedArgs = (TappedEventArgs)e;
        Debug.WriteLine($"Tapped parameter: {tappedArgs.Parameter}");

        Todo? todo = tappedArgs.Parameter as Todo;
        if (todo == null)
        {
            Debug.WriteLine("Todo is null!");
            return;
        }

        Debug.WriteLine($"Todo title: {todo.Title}");
        Navigation.PushAsync(new TodoEditPage(todo));
    }
}
