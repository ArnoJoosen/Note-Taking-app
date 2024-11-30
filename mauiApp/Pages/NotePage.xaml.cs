using System.Collections.ObjectModel;
using Backend.ViewModels;
using Shared.dto;

namespace mauiApp.Pages;

public partial class NotePage : ContentPage {

    NodeViewModel _vm;
    public NotePage(NodeViewModel vm) {
        _vm = vm;
        BindingContext = _vm;
        InitializeComponent();
	}

    protected override void OnAppearing() {

    }
    public async void OnItemTapped(object sender, TappedEventArgs e) {
    }
}
