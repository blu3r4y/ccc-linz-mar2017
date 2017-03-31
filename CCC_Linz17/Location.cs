using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace CCC_Linz17
{
    public class Location
    {
        // meters per second
        public double Speed = 250;

        public string Name;
        public Point2D Position;

        public Location(string name, Point2D position)
        {
            Name = name;
            Position = position;
        }

        public double DistanceTo(Location location)
        {
            return Position.DistanceTo(location.Position);
        }

        public int TravelTimeToWithoutStops(Location location)
        {
            double distance = DistanceTo(location);
            return Utils.RoundNearest(distance/Speed);
        }
    }
}
