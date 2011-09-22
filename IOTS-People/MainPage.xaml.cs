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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // making DataContext
            DataContext = App.ViewModel;
            this.Loaded += MainPage_Loaded;
        }

        /// <summary>
        /// Load data in MainViewModel item and save it to DataContext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                var modelToLoad = (MainViewModel) App.ViewModel;
                modelToLoad.LoadData();
            }
            
        }

        /// <summary>
        /// Executes when leaving the page - saving everything to isolated storage (in SaveData)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var modelToChange = (MainViewModel)App.ViewModel;
            modelToChange.SaveData();
           
        }
    }
}