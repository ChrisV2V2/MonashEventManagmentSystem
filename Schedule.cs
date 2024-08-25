using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProg6221ICE
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int EventID { get; set; }
        public string Venue { get; set; }
        public DateTime TimeSlot { get; set; }
    }
}
