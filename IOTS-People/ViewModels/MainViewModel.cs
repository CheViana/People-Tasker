using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using IOTS_People.ViewModels;


namespace IOTS_People
{
    /// <summary>
    /// Represents all info in app. Fields: list of persons' info (ItemNameViewModel) and list of taskLists(ItemViewModel). 
    /// ItemViewModel with the same index, as ItemNameViewModel, is a list of tasks for this ItemNameViewModel=person.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Tasks = new ObservableCollection<ObservableCollection<ItemViewModel>>();
            this.Names = new ObservableCollection<ItemNameViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        //public ObservableCollection<ItemViewModel> Items { get; private set; }

       /// <summary>
        /// list of taskLists (one tasklist per person)
       /// </summary>
        public ObservableCollection<ObservableCollection<ItemViewModel>> Tasks { get; private set; }

        /// <summary>
        /// list of persons' info
        /// </summary>
        public ObservableCollection<ItemNameViewModel> Names { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Loads (or gives default, if there is no data to be loaded) data: 
        /// Tasks (list of taskLists=ItemViewModels (one taskList per person))
        /// Names (list of Names=ItemNameViewModels (Name = info about person))
        /// </summary>
        public void LoadData()
        {
            //тут подгружаются данные из isolated storage

            if (IsolatedStorageSettings.ApplicationSettings.Contains("Data4"))
            {
                var profiles = (List<Profile>)IsolatedStorageSettings.ApplicationSettings["Data4"];
                var savedModel = MainViewModel.CreateViewModel(profiles);
                this.Names = savedModel.Names;
                this.Tasks = savedModel.Tasks;
            }
            else
            {
                Names.Add(new ItemNameViewModel() {PersonName = "Андрей", Id = 0});
                Names.Add(new ItemNameViewModel() {PersonName = "Витя", Id = 1});
                Names.Add(new ItemNameViewModel() { PersonName = "Дима", Id = 2 });
                Names.Add(new ItemNameViewModel() { PersonName = "Женя", Id = 3 });
                Names.Add(new ItemNameViewModel() { PersonName = "Коля", Id = 4 });
                Names.Add(new ItemNameViewModel() { PersonName = "Куня", Id = 5 });
                foreach (var name in Names)
                {
                    var items = new ObservableCollection<ItemViewModel>();
                    //default tasks
                    items.Add(new ItemViewModel() { TaskName = "First task", Category = "Category: Task"});
                    Tasks.Add(items);
                }

            }
            this.IsDataLoaded = true;
        }
        
        /// <summary>
        /// making Profile objects, that will be saved in AppStorage
        /// </summary>
        public void SaveData()
        {
            List<Profile> profiles = MainViewModel.CreateListOfProfiles(this);
            IsolatedStorageSettings.ApplicationSettings.Remove("Data4");
            IsolatedStorageSettings.ApplicationSettings.Add("Data4", profiles);
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        #region Implementation of INotifyPropertyChanged
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Translation Model into ViewModel and ViewModel into Model
        
        public static List<Profile> CreateListOfProfiles(MainViewModel viewModel)
        {
            List<Profile> profiles = new List<Profile>();
            for (int index = 0; index < viewModel.Tasks.Count; index++)
            {
                var taskList = viewModel.Tasks[index];
                List<Task> taskModels = new List<Task>();
                foreach (var task in taskList)
                {
                    Task taskModel = new Task() { Description = task.TaskDetails, Name = task.TaskName, Category = task.Category};
                    taskModels.Add(taskModel);
                }
                Profile profile = new Profile() { Id = viewModel.Names[index].Id, Name = viewModel.Names[index].PersonName, Tasks = taskModels };
                profiles.Add(profile);
            }
            return profiles;
        }
        public static MainViewModel CreateViewModel(List<Profile> profiles)
        {
            var taskListsVM = new ObservableCollection<ObservableCollection<ItemViewModel>>();
            var namesVM = new ObservableCollection<ItemNameViewModel>();

            foreach (var profile in profiles)
            {
                var taskList = profile.Tasks;
                var taskListVM = new ObservableCollection<ItemViewModel>();
                foreach (var task in taskList)
                {
                    var taskVM = new ItemViewModel() { TaskDetails = task.Description, TaskName = task.Name, Category = task.Category};
                    taskListVM.Add(taskVM);
                }
                taskListsVM.Add(taskListVM);

                
                var nameVM = new ItemNameViewModel() { PersonName = profile.Name, Id = profile.Id };
                namesVM.Add(nameVM);
            }
            var model = new MainViewModel() {IsDataLoaded = false, Names = namesVM, Tasks = taskListsVM};
            return model;
        }
        #endregion

       /// <summary>
       /// Removes item in Tasks
       /// </summary>
       /// <param name="personId">=index of taskList for person with such Id</param>
       /// <param name="taskName">Name of task to remove</param>
        public void RemoveTaskItem(int personId, string taskName)
        {
            for (int index = 0; index < Tasks[personId].Count; index++)
            {
                var task = Tasks[personId][index];
                if (task.TaskName == taskName) 
                {
                    Tasks[personId].Remove(task);
                    break;
                }
            }
        }
    }
}