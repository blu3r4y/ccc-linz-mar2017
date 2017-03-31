using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using MoreLinq;

namespace CCC_Linz17
{
    [DebuggerDisplay("{Name} <{Position.X}, {Position.Y}> {IsStop}")]
    public class Location
    {
        // meters per second
        public static double SpeedHyperloop = 250;
        public static double SpeedWalk = 15;

        public static int StopTime = 200;
        public static int ChangeTime = 300;

        public string Name;
        public Point2D Position;

        public bool IsStop;

        public Location(string name, Point2D position)
        {
            Name = name;
            Position = position;
        }

        public double DistanceTo(Location location)
        {
            return Position.DistanceTo(location.Position);
        }

        public static double ListDistance(List<Location> locations)
        {
            double sum = 0;
            Location pre = locations[0];

            foreach (Location loc in locations)
            {
                double dist = pre.DistanceTo(loc);
                sum += dist;

                pre = loc;
            }

            return sum;
        }

        public double TravelTimeTo(Location location, double speed)
        {
            if (this == location) return 0;

            double distance = DistanceTo(location);
            return distance / speed;
        }

        public double TravelTimeToWithStops(Location location, double speed)
        {
            if (this == location) return 0;

            return TravelTimeTo(location, speed) + StopTime;
        }

        public double TravelTimeToWithStops(Location location, List<Location> pipe, double speed)
        {
            // pipe: sorted list of connected stops

            int startIndex = pipe.FindIndex(l => l == this);
            int endIndex = pipe.FindIndex(l => l == location);

            bool inverse = startIndex > endIndex;
            if (inverse)
            {
                int tmp = startIndex;
                startIndex = endIndex;
                endIndex = tmp;
            }

            double sum = 0;

            Location pre = !inverse ? this : location;

            for (int i = startIndex; i <= endIndex; i++)
            {
                Location stop = pipe[i];

                double dist = pre.TravelTimeToWithStops(stop, speed);
                sum += dist;

                pre = stop;
            }

            return sum;
        }

        public static Location ClosestTo(Location start, List<Location> locations)
        {
            Dictionary<Location, double> distances = locations.ToDictionary(
                location => location, start.DistanceTo);

            return distances.MinBy(pair => pair.Value).Key;
        }
    }
}
