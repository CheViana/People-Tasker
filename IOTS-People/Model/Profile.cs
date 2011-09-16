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
    public class Profile
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public List<Task> Tasks { get; set; }

        public static List<Profile> CreateListOfProfiles (MainViewModel viewModel)
        {
            List<Profile> profiles = new List<Profile>();
            for (int index = 0; index < viewModel.Tasks.Count; index++)
            {
                var taskList = viewModel.Tasks[index];
                List<Task> taskModels = new List<Task>();
                foreach (var task in taskList)
                {
                    Task taskModel = new Task() { Description = task.TaskDetails, Id = new Guid(), Name = task.TaskName };
                    taskModels.Add(taskModel);
                }
                Profile profile = new Profile() { Id = new Guid(), Name = viewModel.Names[index].PersonName, Tasks = taskModels };
                profiles.Add(profile);
            }
            return profiles;
        }
        
    }
}
