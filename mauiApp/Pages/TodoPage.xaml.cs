using Backend.ViewModels;

namespace mauiApp.Pages;

public partial class TodoPage : ContentPage {
	public TodoPage(TodoViewModel _vm) {
		BindingContext = _vm;
		InitializeComponent();
	}
}