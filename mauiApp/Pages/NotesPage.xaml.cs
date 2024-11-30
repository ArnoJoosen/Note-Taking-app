using System.Collections.ObjectModel;
using Backend.ViewModels;
using Shared.dto;

namespace mauiApp.Pages;

public partial class NotePages : ContentPage {

    NodesViewModel _vm;
    public NotePages(NodesViewModel vm) {
        _vm = vm;
        BindingContext = _vm;
        InitializeComponent();
	}

    protected override void OnAppearing() {
        _vm.UpdateNodeList();
    }
    public async void OnItemTapped(object sender, TappedEventArgs e) {
        if (e.Parameter is int tappedItem) {
            await Shell.Current.GoToAsync($"NotePage?id={tappedItem}");
        }
    }
}
