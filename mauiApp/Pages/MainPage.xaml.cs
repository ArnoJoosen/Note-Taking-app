﻿namespace mauiApp.Pages;

using Backend.Services;
using Backend.ViewModels;

public partial class MainPage : ContentPage {
    int count = 0;
    MainViewModel _vm;

    public MainPage(MainViewModel vm) {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing() {
        _vm.UpdateNodeFavorites();
        _vm.UpdateTodoNotDone();
    }

    public async void OnNodeTapped(object sender, TappedEventArgs e) {
        if (e.Parameter is int tappedItem) {
            await Shell.Current.GoToAsync($"NotePage?id={tappedItem}");
        }
    }
}