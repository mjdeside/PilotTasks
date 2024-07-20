using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PilotTasksUpworkService.Model.Common
{
    public enum Status
    {
        [Description("Todo")]
        Todo = 1,
        [Description("Doing")]
        Doing = 2,
        [Description("Done")]
        Done = 3,
    }
}
