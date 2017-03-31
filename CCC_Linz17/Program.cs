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
        public static string LevelPath = Path.Combine(DataPath, @"level3\");

        static void Main(string[] args)
        {
            Console.WriteLine(doLevel2("level3-eg.txt"));

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine(doLevel2($"level3-{i}.txt"));
            }

            Console.Read();
        }

        static int doLevel2(string fileName)
        {
            Utils.Input data = Utils.Read(Path.Combine(LevelPath, fileName));

            List<Location> locations = data.Locations;
            List<Journey> journey = data.Journies;
            Tuple<Location, Location> connection = data.HyperloopConnection;

            int sum = 0;
            int i = 1;
            foreach (Journey jour in journey)
            {
                int hypertime = jour.HyperloopTime(connection);
                //Console.WriteLine(i + " " + jour.Time + " vs " + hypertime + (hypertime < jour.Time ? "YES" : "NO"));
                i++;
                if (hypertime < jour.Time) sum++;
            }

            return sum;
        }
    }
}
