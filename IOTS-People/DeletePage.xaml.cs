using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace IOTS_People
{
    public partial class DeletePage : PhoneApplicationPage
    {
        public DeletePage()
        {
            InitializeComponent();
            //making DataContext
            DataContext = App.ViewModel;
            this.Loaded += MainPage_Loaded;
        }

        /// <summary>
        /// tasks to delete are here. Key in KeyValuePair - Id of person, which has this task, Value  - name of task
        /// </summary>
        private List<KeyValuePair<int, string>> checkedTasks;

        /// <summary>
        /// Load data in MainViewModel item and save it to DataContext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                var modelToLoad = (MainViewModel)App.ViewModel;
                modelToLoad.LoadData();
            }

        }
        /// <summary>
        /// Executes when leaving the page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (checkedTasks != null)
            {
                var modelToChange = (MainViewModel) App.ViewModel;
                foreach (KeyValuePair<int, string> pair in checkedTasks)
                {
                    //removing checked (in CheckBox) tasks
                    modelToChange.RemoveTaskItem(pair.Key, pair.Value);
                }
                modelToChange.SaveData();
            }
        }

       
        /// <summary>
        /// Adds task to delete info to checkedTasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(checkedTasks == null) checkedTasks = new List<KeyValuePair<int, string>>();
            string taskName;
            int personId = GetPersonId(sender, out taskName);
            checkedTasks.Add(new KeyValuePair<int, string>(personId,taskName));
        }

        /// <summary>
        /// Person Id is written into the "Name" field of each CheckBox :(
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="taskName"></param>
        /// <returns></returns>
        private int GetPersonId(object sender, out string taskName)
        {
            CheckBox box = (CheckBox) sender;
            int personId;
            bool errorIndicator = Int32.TryParse(box.Name, out personId);
            if (!errorIndicator)
            {
                throw new ArgumentException("wrong person id");
            }
            taskName = (String)box.Content;
            return personId;
        }


        /// <summary>
        /// Removing unchecked task from checkedTasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Uncheked(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            string taskName;
            int personId = GetPersonId(sender, out taskName);
            checkedTasks.Remove(new KeyValuePair<int, string>(personId, taskName));
        }

        /// <summary>
        /// Canceling. No tasks should be deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            checkedTasks = null;
        }
    }
}