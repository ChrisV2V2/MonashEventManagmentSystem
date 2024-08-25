using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFProg6221ICE
{
    public class Event
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public int MaxAttendees { get; set; }
        public int RegisteredAttendees { get; set; }
    }
}
