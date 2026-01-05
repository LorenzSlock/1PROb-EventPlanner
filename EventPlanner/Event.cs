using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner
{
    public class Event
    {
        public string Name { get; set; }
        public string EventType { get; set; }
        public int Visitors { get; set; }

        public override string ToString()
        {
            return $"{EventType} - {Name} - {Visitors}";
        }
    }
}
