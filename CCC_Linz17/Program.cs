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
        public static string LevelPath = Path.Combine(DataPath, @"level7\");

        static void Main(string[] args)
        {
            Console.WriteLine(doLevel6("level7-eg.txt"));

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine(doLevel6($"level7-{i}.txt"));
            }

            Console.Read();
        }

        static int doLevel6(string fileName)
        {
            Utils.Input data = Utils.Read(Path.Combine(LevelPath, fileName));

            List<Location> locations = data.Locations;
            List<Journey> journies = data.Journies;
            List<List<Location>>  pipes = data.Pipes;
            Location hub = data.Hub;

            return Utils.RoundNearest(journies[0].HyperloopTime(pipes, hub));
        }

        /*
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
        }*/
    }
}
