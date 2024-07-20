using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotTasksUpworkService.Model
{
    public class Tasks : Profile
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime? StartTime { get; set; }
        public int Status { get; set; }

    }
}
