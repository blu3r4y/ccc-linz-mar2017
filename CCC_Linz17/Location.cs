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

        public double TravelTimeTo(Location location, double speed)
        {
            double distance = DistanceTo(location);
            return distance / speed;
        }

        public double TravelTimeToWithStops(Location location, double speed)
        {
            return TravelTimeTo(location, speed) + StopTime;
        }

        public static Location ClosestTo(Location start, List<Location> locations)
        {
            Dictionary<Location, double> distances = locations.ToDictionary(
                location => location, start.DistanceTo);

            return distances.MinBy(pair => pair.Value).Key;
        }
    }
}
