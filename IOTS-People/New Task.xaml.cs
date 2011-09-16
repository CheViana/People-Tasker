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
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        private string checkedName;
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            checkedName = String.Empty;
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            int index;
            switch (checkedName)
            {
                case "Женя":
                    index = 0;
                    break;
                case "Еще кто-то":
                    index = 1;
                    break;
                default:
                    index = App.ViewModel.Tasks.Count;
                    break;

            }
            if (index < App.ViewModel.Tasks.Count)
            {
                App.ViewModel.Tasks[index].Add(new ItemViewModel() { TaskName = EnterNameBox.Text, TaskDetails = EnterdetailsBox.Text });
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox) sender;
            checkedName =(string) box.Content;
        }

    }
}