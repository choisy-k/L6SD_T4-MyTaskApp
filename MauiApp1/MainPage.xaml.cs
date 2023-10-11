using MauiApp1.ViewModel;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            //create a binding context for this page
            //BindingContext = new MainViewModel();
            BindingContext = vm;
        }

    }
}