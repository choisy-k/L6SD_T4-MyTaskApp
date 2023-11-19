using MauiApp1.ViewModel;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        readonly MainViewModel vm;
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            this.vm = vm; //initialise the vm
            //create a binding context for this page
            //BindingContext = new MainViewModel();
            BindingContext = vm;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await vm.LoadItemAsync();
        }
    }
}