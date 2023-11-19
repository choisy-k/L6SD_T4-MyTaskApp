using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Database;
using MauiApp1.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

// for data binding

namespace MauiApp1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        IConnectivity connectivity;
        private readonly DBContext _context;

        public MainViewModel(IConnectivity connectivity, DBContext context)
        {
            //Items = new ObservableCollection<string>();
            this.connectivity = connectivity;
            _context = context;
        }

        //[ObservableProperty]
        //ObservableCollection<string> items;
        //[ObservableProperty]
        //string text;

        [ObservableProperty]
        ObservableCollection<NoteItem> _items = new();
        [ObservableProperty]
        NoteItem? _text = new();

        //indicate operation is running
        [ObservableProperty]
        bool _isBusy;
        [ObservableProperty]
        string _busyText;

        //load items from database
        public async Task LoadItemAsync()
        {
            await ExecuteAsync(async () =>
            {
                if(_context != null)
                {
                    //fetch the items
                    var items = await _context.GetAllAsync<NoteItem>();
                    if(items != null && items.Any())
                    {
                        Items ??= new ObservableCollection<NoteItem>();
                        foreach (var item in items)
                        {
                            Items.Add(item);
                        }
                    }
                }
                
            }, "Fetching notes...");
        }

        [RelayCommand]
        void SetItem(NoteItem? item) => Text = item ?? new();

        [RelayCommand]
        async Task SaveItemAsync()
        {
            if (Text is null) return;

            var (isValid, errorMessage) = Text.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", errorMessage, "Ok");
                return;
            }

            var busyText = Text.Id == 0 ? "Creating note..." : "Updating note...";
            await ExecuteAsync(async () =>
            {
                if(Text.Id == 0)
                {
                    //add a note
                    await _context.AddItemAsync<NoteItem>(Text);
                    Items.Add(Text);
                }
                //there's else for editing item but MyTask doesn't have the option
                SetItemCommand.Execute(new());
            }, busyText);
        }

        [RelayCommand]
        async Task DeleteItemAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if(await _context.DeleteItemByKeyAsync<NoteItem>(id))
                {
                    var item = Items.FirstOrDefault(p => p.Id == id);
                    Items.Remove(item);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Note was not deleted", "Ok");
                }
            }, "Deleting note...");
        }

        /*
        [RelayCommand]
        async void Add()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;

            //checking internet connection
            if(connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No internet connection.", "OK");
                return;
            }

            Items.Add(Text);
            // add the items
            Text = string.Empty;
        }

        [RelayCommand]
        void Delete(string s)
        {
            if(Items.Contains(s))
            {
                Items.Remove(s);
            }
        }
        */

        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
        }

        async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing...";
            try
            {
                await operation?.Invoke();
            }
            catch (Exception ex) {
                Debug.WriteLine($"ExecuteAsync error: {ex}");
            }
            finally
            {
                IsBusy = false;
                BusyText = "Processing...";
            }
        }
    }
}
