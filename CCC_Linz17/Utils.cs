﻿using System;
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
            public Tuple<Location, Location> HyperloopConnection;

            public int Nfaster;

            public Input(List<Location> locations, List<Journey> journies, Tuple<Location, Location> hyperloopConnection, int nfaster)
            {
                Locations = locations;
                Journies = journies;
                HyperloopConnection = hyperloopConnection;
                Nfaster = nfaster;
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

                journies.Add(new Journey(to, from, time));
            }

            i += numJournies;
            /*
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

            i += 1;*/

            int nFaster = int.Parse(totalLines[i]);
            i++;

            // assert
            if (numLocations != locations.Count)
            {
                throw new Exception("Number of Locations does not match.");
            }

            Singleton = new Input(locations, journies, null, nFaster);
            return Singleton;
        }

        public static int RoundNearest(double n)
        {
            return (int) Math.Round(n);
        }
    }
}
