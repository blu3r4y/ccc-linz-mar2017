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
        public static Input Singleton;

        public class Input
        {
            public List<Location> Locations;
            public List<Journey> Journies;
            public List<List<Location>> Pipes;
            public Location Hub;
            public int N;
            public int D;

            public Input(List<Location> locations, List<Journey> journies, List<List<Location>> pipes, Location hub, int n, int d)
            {
                Locations = locations;
                Journies = journies;
                Pipes = pipes;
                Hub = hub;
                N = n;
                D = d;
            }
        }

        public static Input Read(string path)
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

            int numJournies = int.Parse(totalLines[i]);

            i++;

            // read journies 
            var journies = new List<Journey>();
            for (int j = i; j < i + numJournies; j++)
            {
                string[] tofrom = totalLines[j].Split(' ');
                Location to = locations.Find(l => l.Name == tofrom[0]);
                Location from = locations.Find(l => l.Name == tofrom[1]);
                int time = int.Parse(tofrom[2]);

                journies.Add(new Journey(to, from, 0));
            }

            i += numJournies;

            // hub name
            Location hub = locations.Find(l => l.Name == totalLines[i]); ;
            i++;

            int n = int.Parse(totalLines[i]);
            i++;
            int d = int.Parse(totalLines[i]);
            i++;

            /*
            int numPipes = int.Parse(totalLines[i]);
            i++;

            // read hyperloop connections
            var connections = new List<List<Location>>();
            for (int j = i; j < i + numPipes; j++)
            {
                var pipe = new List<Location>();

                string[] connStr = totalLines[j].Split(' ');
                for (int s = 1; s < connStr.Length; s++)
                {
                    Location loc = locations.Find(l => l.Name == connStr[s]);
                    loc.IsStop = true;
                    pipe.Add(loc);
                }

                connections.Add(pipe);
            }*/

            /*
            var connections = new List<Location>();
            for (int j = i; j <= i; j++)
            {
                string[] tofrom = totalLines[j].Split(' ');
                Location to = locations.Find(l => l.Name == tofrom[0]);
                Location from = locations.Find(l => l.Name == tofrom[1]);

                // mark as stop point
                to.IsStop = true;
                from.IsStop = true;

                connections.Add(new List<Location>(to, from));
            }
            */

            i += 1;

            // int nFaster = int.Parse(totalLines[i]);
            // i++;

            // assert
            if (numLocations != locations.Count)
            {
                throw new Exception("Number of Locations does not match.");
            }

            Singleton = new Input(locations, journies, null, hub, n, d);
            return Singleton;
        }

        public static int RoundNearest(double n)
        {
            return (int) Math.Round(n);
        }

        public static string LocToStr(List<Location> list)
        {
            return list.Count + " " + string.Join(" ", list.Select(l => l.Name));
        }

        public static string LocToStr(List<List<Location>> list)
        {
            return list.Count + " " + string.Join(" ", list.Select(LocToStr));
        }
    }
}
