using MauiApp1.ViewModel;

namespace MauiApp1;

public partial class DetailPage : ContentPage
{
	public DetailPage()
	{
		InitializeComponent();
        
        var vm = new CreateTaskViewModel();
		BindingContext = vm;
    }
}