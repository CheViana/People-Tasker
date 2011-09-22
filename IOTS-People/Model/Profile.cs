using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IOTS_People.ViewModels;

namespace IOTS_People
{
    /// <summary>
    /// objects of this class are used to save and load data in this application.
    ///  Profile per person consists of person's name, id and his tasks.
    /// </summary>
    public class Profile
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Task> Tasks { get; set; }

        
        
    }
}
