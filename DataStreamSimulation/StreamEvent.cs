using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStreamSimulation
{
    /// <summary>
    /// Event Class for simulation
    /// </summary>
    public class StreamEvent
    {
        public double Time { get; set; }
        public string Type { get; private set; }

        public StreamEvent(double time, string type)
        {
            Time = time;
            Type = type;
        }
    }
}
