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
        public static string LevelPath = Path.Combine(DataPath, @"level6\");

        static void Main(string[] args)
        {
            Console.WriteLine(doLevel6("level6-4.txt"));

            for (int i = 1; i <= 4; i++)
            {
                // Console.WriteLine(doLevel6($"level6-{i}.txt"));
            }

            Console.Read();
        }

        static string doLevel6(string fileName)
        {
            Utils.Input data = Utils.Read(Path.Combine(LevelPath, fileName));

            List<Location> locations = data.Locations;
            List<Journey> journies = data.Journies;
            // List<Location>  pipe = data.HyperloopConnections;
            int n = data.N; // num faster
            int d = data.D; // max len



            for (int x = 0; x < 8; x++)
            {
                Thread t = new Thread(() =>
                {
                    Random rnd = new Random();
                    while (true)
                    {
                        int numStops = Math.Min(rnd.Next(1, 100 + 1), locations.Count);

                        var rndList = randomList(rnd, 0, locations.Count, numStops);
                        List<Location> pipe = rndList.Select(i => locations[i]).ToList();

                        if (check(journies, pipe, n, d)) break;
                    }
                    
                });

                t.Start();
            }
            

            return "Started.";
        }

        public static IEnumerable<int> randomList(Random rnd, int min, int maxEx, int n)
        {
            HashSet<int> rndList = new HashSet<int>();

            while (rndList.Count < n)
            {
                int num = rnd.Next(min, maxEx);
                if (!rndList.Contains(num))
                    rndList.Add(num);
            }

            return rndList;;
        }

        public static bool check(List<Journey> journies, List<Location> pipe, int n, int d)
        {
            double len = Location.ListDistance(pipe);
            int stops = pipe.Count;

            if (stops > 100)
            {
                throw new Exception("too many stops");
                return false;
            }

            if (len > d)
            {
                // throw new Exception("too long");
                return false;
            }

            int nCalc = numFaster(journies, pipe);
            if (nCalc >= n)
            {
                Console.WriteLine("FOUND --> " + Utils.LocToStr(pipe));
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int numFaster(List<Journey> journies, List<Location> pipe)
        {
            int sum = 0;
            foreach (Journey jour in journies)
            {
                int hypertime = jour.HyperloopTime(pipe);
                if (hypertime < jour.Time) sum++;
            }

            return sum;
        }
    }
}
