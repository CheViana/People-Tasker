using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IOTS_People.ViewModels
{
    /// <summary>
    /// Represents person's Id and Name to be used in data binding
    /// </summary>
    public class ItemNameViewModel : INotifyPropertyChanged
    {
        private string personName;
        private int id;
        
        public string PersonName
        {
            get
            {
                return personName;
            }
            set
            {
                if (value != personName)
                {
                    personName = value;
                    NotifyPropertyChanged("PersonName");
                }
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    NotifyPropertyChanged("PersonId");
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
