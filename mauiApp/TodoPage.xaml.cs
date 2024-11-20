using Backend.ViewModels;

namespace mauiApp;

public partial class TodoPage : ContentPage
{
	public TodoPage(TodoViewModel _vm)
	{
		BindingContext = _vm;
		InitializeComponent();
	}
}