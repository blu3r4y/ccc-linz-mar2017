using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Newtonsoft.Json;

namespace CCC_Linz17
{
    internal class Program
    {
        public static string DataPath = @"C:\data\Dropbox\Projekte\Code\CCC_Linz17\Data\";
        public static string LevelPath = Path.Combine(DataPath, @"level8\");

        static void Main(string[] args)
        {
            Console.WriteLine(doLevel8("level8-1.txt"));

            for (int i = 1; i <= 4; i++)
            {
                // Console.WriteLine(doLevel8($"level8-{i}.txt"));
            }

            Console.Read();
        }

        static int doLevel8(string fileName)
        {
            Utils.Input data = Utils.Read(Path.Combine(LevelPath, fileName));

            List<Location> locations = data.Locations;
            List<Journey> journies = data.Journies;
            // List<List<Location>>  pipes = data.Pipes;
            Location hub = data.Hub;
            int n = data.N; // num journies
            int d = data.D; // max len

            int hubIndex = locations.IndexOf(hub);

            for (int x = 0; x < 8; x++)
            {
                Thread t = new Thread(() =>
                {
                    Random rnd = new Random();
                    while (true)
                    {
                        int numPipes = rnd.Next(2, 4);

                        var pipes = new List<List<Location>>();
                        for (int p = 0; p < numPipes; p++)
                        {
                            int numStops = Math.Min(rnd.Next(3, 15 + 1), locations.Count);

                            // n-1 stops
                            List<int> rndList = randomList(rnd, 0, locations.Count, numStops - 1, hubIndex).ToList();
                            // add hub somewhere
                            int idx = rnd.Next(0, numStops);
                            rndList.Insert(idx, hubIndex);

                            List<Location> pipe = rndList.Select(i => locations[i]).ToList();

                            pipes.Add(pipe);
                        }

                        if (check(journies, pipes, n, d, hub)) break;
                    }

                });

                t.Start();
            }

            return 0;
        }

        
        public static IEnumerable<int> randomList(Random rnd, int min, int maxEx, int n, int hubIndex)
        {
            HashSet<int> rndList = new HashSet<int>();

            while (rndList.Count < n)
            {
                int num = rnd.Next(min, maxEx);
                if (!rndList.Contains(num) && num != hubIndex)
                    rndList.Add(num);
            }

            return rndList;;
        }

        public static bool check(List<Journey> journies, List<List<Location>> pipes, int n, int d, Location hub)
        {
            double len = pipes.Select(Location.ListDistance).Sum();
            int stops = pipes.Select(list => list.Count).Sum();;

            if (stops > 100)
            {
                Console.WriteLine("too many stops " + stops + " > " + "100");
                return false;
            }

            if (len > d)
            {
                // throw new Exception("too long");
                // Console.WriteLine("too long " + len + " > " + d);
                return false;
            }

            int nCalc = numFaster(journies, pipes, hub);
            if (nCalc >= n)
            {
                Console.WriteLine("FOUND --> " + Utils.LocToStr(pipes));
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int numFaster(List<Journey> journies, List<List<Location>> pipes, Location hub)
        {
            int sum = 0;
            foreach (Journey jour in journies)
            {
                int hypertime = jour.HyperloopTime(pipes, hub);
                if (hypertime < jour.Time) sum++;
            }

            return sum;
        }
    }
}
