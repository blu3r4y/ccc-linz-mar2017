using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using Newtonsoft.Json;

namespace CCC_Linz17
{
    internal class Program
    {
        public static string DataPath = @"C:\data\Dropbox\Projekte\Code\CCC_Linz17\Data\";
        public static string LevelPath = Path.Combine(DataPath, @"level4\");

        static void Main(string[] args)
        {
            Console.WriteLine(doLevel4("level4-eg.txt"));

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine(doLevel4($"level4-{i}.txt"));
            }

            Console.Read();
        }

        static string doLevel4(string fileName)
        {
            Utils.Input data = Utils.Read(Path.Combine(LevelPath, fileName));

            List<Location> locations = data.Locations;
            List<Journey> journey = data.Journies;
            Tuple<Location, Location> connection = data.HyperloopConnection;
            int nfaster = data.Nfaster;

            foreach (Location a in locations)
            {
                foreach (Location b in locations)
                {
                    if (a != b)
                    {
                        int isFaster = numFaster(journey, new Tuple<Location, Location>(a, b));
                        if (isFaster >= nfaster)
                        {
                            return a.Name + " " + b.Name;
                        }
                    }
                }
            }

            return null;
        }

        public static int numFaster(List<Journey> journies, Tuple<Location, Location> connection)
        {
            int sum = 0;
            foreach (Journey jour in journies)
            {
                int hypertime = jour.HyperloopTime(connection);
                if (hypertime < jour.Time) sum++;
            }

            return sum;
        }
    }
}
