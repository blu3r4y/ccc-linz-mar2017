using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_Linz17
{
    [DebuggerDisplay("{To} -> {From} in {Time}")]
    public class Journey
    {
        public Location To;
        public Location From;

        public int Time;

        public Journey(Location to, Location from, int time)
        {
            To = to;
            From = from;
            Time = time;
        }

        public int HyperloopTime(Tuple<Location, Location> connection)
        {
            Location closestStart = Location.ClosestTo(To,
                new List<Location> { connection.Item1, connection.Item2 });
            Location closestEnd = closestStart == connection.Item1 ? connection.Item2 : connection.Item1;

            double walktime1 = To.TravelTimeTo(closestStart, Location.SpeedWalk);
            double traveltime = closestStart.TravelTimeToWithStops(closestEnd, Location.SpeedHyperloop);
            double walktime2 = closestEnd.TravelTimeTo(From, Location.SpeedWalk);

            int result = Utils.RoundNearest(walktime1 + walktime2 + traveltime);

            return result;
        }
    }
}
