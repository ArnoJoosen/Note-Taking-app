using System.Collections.ObjectModel;
using Backend.ViewModels;
using Shared.dto;

namespace mauiApp.Pages;

public partial class NotePages : ContentPage {

    NotesViewModel _vm;
    public NotePages(NotesViewModel vm) {
        _vm = vm;
        BindingContext = _vm;
        _vm.NotFound += OnNotFound;
        _vm.ConnectionError += OnConnectionError;
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
    public void OnConnectionError() {
        Navigation.PopToRootAsync(); // go back to the main page if there is a connection error
    }
    public void OnNotFound(int id) {
        DisplayAlert("Note not found", $"Note with id {id} was not found", "OK");
        _vm.UpdateNodeList();
    }
}
