using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotTasksUpworkService.Model
{
    public class TasksView : Tasks
    {
        public string TasksAssignee { get; set; }
        public string StartTimeDisplay { get; set; }
        public string StatusDisplay { get; set; }
    }
}
