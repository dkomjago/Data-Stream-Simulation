using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStreamSimulation
{
    static class StreamSimulation
    {
        static Random random = new Random();

        /// <summary>
        /// Add randomized time to argument (Exponential distribution)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private static double AddRandomTime(double time)
        {
            var lambda = 0.05;
            double newTime = (-1.0 / lambda) * Math.Log(random.NextDouble());
            return newTime + time;
        }

        /// <summary>
        /// Main simulation loop
        /// </summary>
        /// <returns>All recorded event data</returns>
        public static List<SimulationData> Simulate()
        {
            List<SimulationData> simulationData = new List<SimulationData>();
            List<StreamEvent> streamEvents = new List<StreamEvent>();
            #region Customizable simulation starting data
            double packetSize = 3.5;
            const double HQ = 7;
            const double MQ = 3.5;
            const double LQ = 1;
            double currentTime = 0;
            const double simulationLength = 450;
            double eventStartTime = 0;
            double bufferLength = 0;
            double bandWidth = 5;
            #endregion

            #region Starting Events
            StreamEvent streamChangeEvent = new StreamEvent(AddRandomTime(currentTime), "StreamChange");
            streamEvents.Add(streamChangeEvent);

            StreamEvent downloadEvent = new StreamEvent(currentTime + (packetSize / bandWidth), "FragmentDownloaded");
            streamEvents.Add(downloadEvent);

            simulationData.Add(new SimulationData(currentTime, bufferLength, bandWidth, packetSize));
            #endregion

            while (currentTime < simulationLength)
            {
                streamEvents = streamEvents.OrderBy(x => x.Time).ToList(); //Order queue by time with each iteration

                if (streamEvents[0].Type == "FragmentDownloaded")
                {
                    eventStartTime = currentTime;
                }

                currentTime = streamEvents[0].Time;

                //Change streaming speed,adjust fragment download completion time
                if (streamEvents[0].Type == "StreamChange") 
                {
                    var oldBandWidth = bandWidth;
                    bandWidth = random.NextDouble() * 10;
                    if (streamEvents[1].Type == "FragmentDownloaded")
                    {
                        streamEvents[1].Time = currentTime + ((packetSize - (streamEvents[1].Time - currentTime) * oldBandWidth) / bandWidth);
                    }
                    streamChangeEvent = new StreamEvent(AddRandomTime(currentTime), "StreamChange");
                    streamEvents.Add(streamChangeEvent);
                }
                //Fragment has been downloaded, add fragment length to buffer
                else if (streamEvents[0].Type == "FragmentDownloaded")
                {
                    streamChangeEvent = new StreamEvent(currentTime + (packetSize / bandWidth), "FragmentDownloaded");
                    streamEvents.Add(streamChangeEvent);
                    bufferLength++;
                    bufferLength = bufferLength - (currentTime - eventStartTime);
                    if (bufferLength > 30) bufferLength = 30;
                    if (bufferLength < 0) bufferLength = 0;
                    //Adjust fragment quality to bandwidth
                    if (bandWidth < 3.5) packetSize = LQ;
                    else if (bandWidth > 7) packetSize = HQ;
                    else packetSize = MQ;
                }
                //Record event time, get next
                simulationData.Add(new SimulationData(streamEvents[0].Time, bufferLength, bandWidth, packetSize));
                streamEvents.RemoveAt(0);
            }
            return simulationData;
        }
    }
}
