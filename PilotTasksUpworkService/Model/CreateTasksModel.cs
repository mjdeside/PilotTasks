using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotTasksUpworkService.Model
{
    public class CreateTasksModel : Tasks
    {
        public List<ProfileDropdown> ProfileList { get; set; }
        public List<StatusDropDown> StatusList { get; set; }
    }
    public class StatusDropDown
    {
        public int StatusId { get; set; }
        public string StatusDisplay { get; set; } 
    }
    public class ProfileDropdown { 
        public string DisplayName { get; set; }
        public int ProfileId { get; set; }
    }
}
