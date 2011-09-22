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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace IOTS_People
{
    public partial class New_Task : PhoneApplicationPage
    {
        public New_Task()
        {
            InitializeComponent();
            //making DataContext
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }
        /// <summary>
        /// Name of person to whom taskList the task is added
        /// </summary>
        private string checkedName;

        /// <summary>
        /// Category of new task
        /// </summary>
        private string category;

        /// <summary>
        /// Indicates, wheter the "Cancel" button was pressed
        /// </summary>
        private bool cancel;

        /// <summary>
        /// Load data in MainViewModel item and save it to DataContext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            checkedName = String.Empty;
        }
        /// <summary>
        /// Executes when leaving the page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (!cancel) //if navigation is caused not by pressing the Cancel button
            {
                //default index is the size of App.ViewModel.Tasks - element with default index doesn't exist
                int index = App.ViewModel.Tasks.Count;

                //ViewModel
                MainViewModel model = (MainViewModel) DataContext;

                //looking for the person, to whom taskList a new task should be added
                for (int i = 0; i < model.Names.Count; i++)
                {
                    
                    if (checkedName == model.Names[i].PersonName)
                    {
                        index = model.Names[i].Id; //saving the index(Id) of the person, to whom taskList a new task should be added
                    }
                }
                //if index is not default
                if (index < App.ViewModel.Tasks.Count)
                {
                    //Adding a task  to taskList of "checked(in CheckBox)" person
                    App.ViewModel.Tasks[index].Add(new ItemViewModel()
                                                       {TaskName = EnterNameBox.Text, Category = category});
                }
                App.ViewModel.SaveData();
            }
        }

        /// <summary>
        /// saves a category, chosed by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton box = (RadioButton)sender;
            category = "Category: "+(string)box.Content;
        }


        /// <summary>
        /// saves a person's name, chosed by user - to that person user wants to add a task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton box = (RadioButton)sender;
            checkedName =(string) box.Content;
        }


        /// <summary>
        /// sets Cancel Indicator to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            cancel = true;
        }

        

    }
}