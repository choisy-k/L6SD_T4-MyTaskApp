using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp1.ViewModel
{
    [QueryProperty("Text", "Text")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        string text;

        [RelayCommand]
        async Task GoBack()
        {
            // dot indicates navigates backwards, can also add /[filename]
            await Shell.Current.GoToAsync("..");
        }
    }
}
