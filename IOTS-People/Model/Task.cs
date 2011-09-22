using System;
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
    /// objects of this class are used to save and load data in this application.
    /// Task have Name, description(=details) and Category (report or task)
    /// </summary>
    public class Task
    {
       
        public String Name { get; set; }
        public String Description { get; set; }
        public string Category { get; set; }
    }
}
