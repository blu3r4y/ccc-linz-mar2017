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
        public static string LevelPath = Path.Combine(DataPath, @"level5\");

        static void Main(string[] args)
        {
            Console.WriteLine(doLevel4("level5-eg.txt"));

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine(doLevel4($"level5-{i}.txt"));
            }

            Console.Read();
        }

        static int doLevel4(string fileName)
        {
            Utils.Input data = Utils.Read(Path.Combine(LevelPath, fileName));

            List<Location> locations = data.Locations;
            List<Journey> journey = data.Journies;
            List<Location>  connections = data.HyperloopConnections;

            

            return Utils.RoundNearest(journey[0].HyperloopTime(connections));
        }

        /*public static int numFaster(List<Journey> journies, Tuple<Location, Location> connection)
        {
            int sum = 0;
            foreach (Journey jour in journies)
            {
                int hypertime = jour.HyperloopTime(connection);
                if (hypertime < jour.Time) sum++;
            }

            return sum;
        }*/
    }
}
