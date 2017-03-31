using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace CCC_Linz17
{
    public static class Utils
    {
        public static Tuple<List<Location>, Tuple<Location, Location>, Tuple<Location, Location>> Read(string path)
        {
            int i = 0;

            string[] totalLines = File.ReadAllLines(path);
            int numLocations = int.Parse(totalLines[i]);

            i++;
            
            // read locations
            var locations = new List<Location>();
            for (int j = i; j < i + numLocations; j++)
            {
                string line = totalLines[j];
                string[] splitted = line.Split(' ');

                string name = splitted[0];
                int x = int.Parse(splitted[1]);
                int y = int.Parse(splitted[2]);

                locations.Add(new Location(name, new Point2D(x, y)));
            }

            i += numLocations;

            // read journies 
            var journies = new List<Tuple<Location, Location>>();
            for (int j = i; j <= i; j++)
            {
                string[] tofrom = totalLines[j].Split(' ');
                Location to = locations.Find(l => l.Name == tofrom[0]);
                Location from = locations.Find(l => l.Name == tofrom[1]);

                journies.Add(new Tuple<Location, Location>(to, from));
            }

            i += 1;

            // read hyperloop connections
            var connections = new List<Tuple<Location, Location>>();
            for (int j = i; j <= i; j++)
            {
                string[] tofrom = totalLines[j].Split(' ');
                Location to = locations.Find(l => l.Name == tofrom[0]);
                Location from = locations.Find(l => l.Name == tofrom[1]);

                // mark as stop point
                to.IsStop = true;
                from.IsStop = true;

                connections.Add(new Tuple<Location, Location>(to, from));
            }

            i += 1;
            
            // assert
            if (numLocations != locations.Count)
            {
                throw new Exception("Number of Locations does not match.");
            }

            return new Tuple<List<Location>, Tuple<Location, Location>, Tuple<Location, Location>>(locations, journies[0], connections[0]);
        }

        public static int RoundNearest(double n)
        {
            return (int) Math.Round(n);
        }
    }
}
