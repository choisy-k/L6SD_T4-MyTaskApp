using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

// for data binding

namespace MauiApp1.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainViewModel : ObservableObject
    {
        //IConnectivity connectivity;
        public MainViewModel()
        {
            DummyData();

            //this.connectivity = connectivity;

            //initialise task collection within each categories
            foreach (var category in Categories)
            {
                category.Tasks = new ObservableCollection<TheTasks>(Tasks.Where(task => task.CategoryId == category.Id));
                category.UpdateTotalTasks();
            }
        }

        [ObservableProperty]
        ObservableCollection<Category> categories;
        [ObservableProperty]
        Category selectedCategory;

        [ObservableProperty]
        ObservableCollection<TheTasks> tasks;

        [ObservableProperty]
        string categoryName;
        [ObservableProperty]
        string categoryColor;
        [ObservableProperty]
        int completedTask;
        [ObservableProperty]
        int totalTasks;
        [ObservableProperty]
        bool isSelected;
        [ObservableProperty]
        float percentage;

        [ObservableProperty]
        string taskName;
        [ObservableProperty]
        bool isCompleted;
        [ObservableProperty]
        int categoryId;
        [ObservableProperty]
        string taskColor;

        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                var tasks = from t in Tasks
                            where t.CategoryId == c.Id
                            select t;

                var completed = from t in tasks
                                where t.IsCompleted == true
                                select t;

                var noCompleted = from t in tasks
                                  where t.IsCompleted == false
                                  select t;
                c.CompletedTask = completed.Count();
                c.TotalTasks = tasks.Count();
                c.Percentage = (float)completed.Count()/(float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                var catColor =
                    (
                        from c in Categories
                        where c.Id == t.CategoryId
                        select c.CategoryColor
                    ).FirstOrDefault();
                t.TaskColor = catColor;
            }
        }

        [RelayCommand]
        async void EditCategory(Category category)
        {
            if (Categories.Contains(category))
            {
                string editLabel = await App.Current.MainPage.DisplayPromptAsync("Edit", "Write the new category name");
                category.CategoryName = editLabel;
                UpdateData();
            }
        }

        [RelayCommand]
        async void DeleteCategory(Category category)
        {
            // Still need to be changed
            if (Categories.Contains(category))
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Warning!", "Deleting a category will delete all tasks within it. Do you still wish to proceed?", "Yes","No");
                if (answer == true)
                {
                    //delete all related tasks before the category itself
                    foreach(var t in category.Tasks.ToList())
                    {
                        category.Tasks.Remove(t);
                    }
                    Categories.Remove(category);
                    UpdateData();
                }
                else return;
            }
        }

        [RelayCommand]
        async void EditTask(TheTasks task)
        {
            if (Tasks.Contains(task))
            {
                string editLabel = await App.Current.MainPage.DisplayPromptAsync("Edit", "Write the new task name");
                task.TaskName = editLabel;
                UpdateData();
            }
        }

        [RelayCommand]
        void DeleteTask(TheTasks task)
        {
            if(Tasks.Contains(task))
            {
                Tasks.Remove(task);
                UpdateData();
            }
        }

        void DummyData()
        {
            Categories = new ObservableCollection<Category>()
            {
                new Category
                {
                    Id = 0,
                    CategoryName = "Green Category",
                    CategoryColor = "#00A36C"
                },
                new Category
                {
                    Id = 1,
                    CategoryName = "Violet Category",
                    CategoryColor = "#8F00FF"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Brown Category",
                    CategoryColor = "#72674e"
                },
            };

            Tasks = new ObservableCollection<TheTasks>()
            {
                new TheTasks
                {
                    TaskName = "Light green",
                    IsCompleted = false,
                    CategoryId = 0,
                },
                new TheTasks
                {
                    TaskName = "Teal",
                    IsCompleted = false,
                    CategoryId = 0,
                },
                new TheTasks
                {
                    TaskName = "Deep violet",
                    IsCompleted = false,
                    CategoryId = 1,
                },
                new TheTasks
                {
                    TaskName = "Forest",
                    IsCompleted = false,
                    CategoryId = 0,
                },
                new TheTasks
                {
                    TaskName = "Earthy",
                    IsCompleted = false,
                    CategoryId = 2,
                },
                new TheTasks
                {
                    TaskName = "Purple?",
                    IsCompleted = false,
                    CategoryId = 1,
                },
            };
            UpdateData();
        }

        //================================
    }
}
