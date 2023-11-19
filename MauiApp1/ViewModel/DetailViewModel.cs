using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Database;
using MauiApp1.Model;
using SQLitePCL;

namespace MauiApp1.ViewModel
{
    [QueryProperty("Text", "Text")]
    public partial class DetailViewModel : ObservableObject
    { 
        private readonly DBContext _context;

        public DetailViewModel(DBContext context)
        {
            _context = context;
        }
        [ObservableProperty]
        NoteItem text;

        public async Task LoadItemAsync()
        {
            if(_context != null)
            {
                
            }
        }

        [RelayCommand]
        async Task GoBack()
        {
            // dot indicates navigates backwards, can also add /[filename]
            await Shell.Current.GoToAsync("..");
        }
    }
}
