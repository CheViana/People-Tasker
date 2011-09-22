using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IOTS_People
{
    /// <summary>
    /// Represents a name, details and category of task to be used in data binding
    /// </summary>
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string taskName;
        /// <summary>
        /// Name property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string TaskName
        {
            get
            {
                return taskName;
            }
            set
            {
                if (value != taskName)
                {
                    taskName = value;
                    NotifyPropertyChanged("TaskName");
                }
            }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set 
            {
                if (value != category)
                {
                    category = value;
                    NotifyPropertyChanged("Category");
                }
            }
        }
        private string taskDetails;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string TaskDetails
        {
            get
            {
                return taskDetails;
            }
            set
            {
                if (value != taskDetails)
                {
                    taskDetails = value;
                    NotifyPropertyChanged("TaskDetails");
                }
            }
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
    }
}