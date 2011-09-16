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

        public ObservableCollection<ObservableCollection<ItemViewModel>> Tasks { get; private set; }

        public ObservableCollection<ItemNameViewModel> Names { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            //тут подгружать данные с isolated storage

            if (IsolatedStorageSettings.ApplicationSettings.Contains("Data3"))
            {
                var profiles = (List<Profile>)IsolatedStorageSettings.ApplicationSettings["Data3"];
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
                    items.Add(new ItemViewModel() { TaskName = "First task", TaskDetails = "--------" });
                    Tasks.Add(items);
                }

            }
            this.IsDataLoaded = true;
        }

        public void SaveData()
        {
            //making Profile objects, that will be saved in AppStorage

            List<Profile> profiles = Profile.CreateListOfProfiles(this);
            IsolatedStorageSettings.ApplicationSettings.Remove("Data3");
            IsolatedStorageSettings.ApplicationSettings.Add("Data3", profiles);
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
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
                    var taskVM = new ItemViewModel() { TaskDetails = task.Description, TaskName = task.Name };
                    taskListVM.Add(taskVM);
                }
                taskListsVM.Add(taskListVM);

                
                var nameVM = new ItemNameViewModel() { PersonName = profile.Name, Id = profile.Id };
                namesVM.Add(nameVM);
            }
            var model = new MainViewModel() {IsDataLoaded = false, Names = namesVM, Tasks = taskListsVM};
            return model;
        }
    }
}