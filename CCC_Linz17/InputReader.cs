using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace CCC_Linz17
{
    public static class InputReader
    {
        public static Tuple<List<Location>, Tuple<Location, Location>> Read(string path)
        {
            var list = new List<Location>();

            string[] lines = File.ReadAllLines(path);
            int num = int.Parse(lines[0]);

            // read locations
            for (var i = 1; i < lines.Length - 1; i++)
            {
                string line = lines[i];
                string[] splitted = line.Split(' ');

                string name = splitted[0];
                int x = int.Parse(splitted[1]);
                int y = int.Parse(splitted[2]);

                list.Add(new Location(name, new Point2D(x, y)));
            }

            // read to, from
            string[] tofrom = lines[lines.Length - 1].Split(' ');
            Location to = list.Find(l => l.Name == tofrom[0]);
            Location from = list.Find(l => l.Name == tofrom[1]);

            // assert
            if (num != list.Count)
            {
                throw new Exception("Number of Locations does not match.");
            }

            return new Tuple<List<Location>, Tuple<Location, Location>>(list, new Tuple<Location, Location>(to, from));
        }
    }
}
