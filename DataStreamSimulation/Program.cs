using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStreamSimulation
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<SimulationData> output = StreamSimulation.Simulate();
            ExcelManager.Write(output);
        }
    }
}

