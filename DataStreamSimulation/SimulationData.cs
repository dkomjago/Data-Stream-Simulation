using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStreamSimulation
{
    /// <summary>
    /// Class for recording simulation output data
    /// </summary>
    public class SimulationData
    {
        public double Time { get; private set; }
        public double Buffer { get; private set; }
        public double BandWidth { get; private set; }
        public double BitRate { get; private set; }
        public SimulationData(double time, double buffer, double bandWidth, double bitRate)
        {
            Time = time;
            Buffer = buffer;
            BandWidth = bandWidth;
            BitRate = bitRate;
        }
    }
}
