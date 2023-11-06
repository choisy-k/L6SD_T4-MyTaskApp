using CommunityToolkit.Maui.Views;
using MauiApp1.ViewModel;
using System.Diagnostics;
using MauiApp1.Models;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        MainViewModel vm = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
            //create a binding context for this page
            BindingContext = vm;
        }

        private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            TheTasks task = (TheTasks)checkBox.BindingContext;

            // Update the IsCompleted property based on the CheckBox state
            task.IsCompleted = e.Value;

            vm.UpdateData();
        }

        private void addBtn_Clicked(object sender, EventArgs e)
        {
            var taskView = new DetailPage()
            {
                BindingContext = new CreateTaskViewModel
                {
                    Tasks = vm.Tasks,
                    Categories = vm.Categories
                }
            };
            if (taskView != null)
            {
                Navigation.PushAsync(taskView);
            }
            else
            {
                DisplayAlert("Error!", "Loading this page", "Ok");
            }
        }
    }
}