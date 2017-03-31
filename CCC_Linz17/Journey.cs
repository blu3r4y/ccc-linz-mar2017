using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_Linz17
{
    [DebuggerDisplay("{End} -> {Start} in {Time}")]
    public class Journey
    {
        public Location End;
        public Location Start;

        public int Time;

        public Journey(Location start, Location end, int time)
        {
            Start = start;
            End = end;
            Time = time;
        }

        public int HyperloopTime(List<List<Location>> pipes, Location hub)
        {
            List<Location> allPipes = pipes.SelectMany(p => p).Distinct().ToList();

            // stops
            Location closestStart = Location.ClosestTo(Start, allPipes);
            Location closestEnd = Location.ClosestTo(End, allPipes);

            // pipe index
            int startPipe = pipes.FindIndex(pipelist => pipelist.Contains(closestStart));
            int endPipe = pipes.FindIndex(pipelist => pipelist.Contains(closestEnd));

            List<Location> startPipeList = pipes[startPipe];
            List<Location> endPipeList = pipes[endPipe];

            if (startPipe != endPipe)
            {
                double walktime1 = Start.TravelTimeTo(closestStart, Location.SpeedWalk);
                double traveltime1 = closestStart.TravelTimeToWithStops(hub, startPipeList, Location.SpeedHyperloop);
                double traveltime2 = hub.TravelTimeToWithStops(closestEnd, endPipeList, Location.SpeedHyperloop);
                double walktime2 = closestEnd.TravelTimeTo(End, Location.SpeedWalk);

                double changeTime = (closestStart != hub && closestEnd != hub) ? Location.ChangeTime : 0;

                int result = Utils.RoundNearest(walktime1 + walktime2 + traveltime1 + traveltime2 + changeTime);

                return result;
            }
            else
            {
                List<Location> singlePipe = pipes[startPipe];

                double walktime1 = Start.TravelTimeTo(closestStart, Location.SpeedWalk);
                double traveltime = closestStart.TravelTimeToWithStops(closestEnd, singlePipe, Location.SpeedHyperloop);
                double walktime2 = closestEnd.TravelTimeTo(End, Location.SpeedWalk);

                int result = Utils.RoundNearest(walktime1 + walktime2 + traveltime);

                return result;
            }
        }
    }
}
