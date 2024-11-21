using Backend.ViewModels;

namespace mauiApp.Pages;

public partial class TodoPage : ContentPage {

	TodoViewModel _vm;
	public TodoPage(TodoViewModel vm) {
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
}