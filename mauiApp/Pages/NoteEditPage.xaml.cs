using Backend.Services;
using Backend.ViewModels;
using Shared.Models;

namespace mauiApp.Pages;

[QueryProperty(nameof(NodeId), "id")]
public partial class NoteEditPage : ContentPage {
    private int _Id;
    NoteEditViewModel _vm;
    IApiNoteService _api;
    public int NodeId {
        get => _Id;
        set {
            _Id = value;
            LoadNote();
        }
    }

	public NoteEditPage(IApiNoteService api) {
        _api = api;
        InitializeComponent();
	}

	private void LoadNote() {
        _vm = new(_api, _Id);
        BindingContext = _vm;
    }

    public void OnSaveClicked(object sender, EventArgs e) {
        _vm.Save();
        Navigation.PopAsync();
    }
}
