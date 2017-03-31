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

        public int HyperloopTime(List<Location> connections)
        {
            Location closestStart = Location.ClosestTo(Start, connections);
            Location closestEnd = Location.ClosestTo(End, connections);

            double walktime1 = Start.TravelTimeTo(closestStart, Location.SpeedWalk);
            double traveltime = closestStart.TravelTimeToWithStops(closestEnd, connections, Location.SpeedHyperloop);
            double walktime2 = closestEnd.TravelTimeTo(End, Location.SpeedWalk);

            int result = Utils.RoundNearest(walktime1 + walktime2 + traveltime);

            return result;
        }
    }
}
