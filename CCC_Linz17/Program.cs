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
        public static string LevelPath = Path.Combine(DataPath, @"level2\");

        static void Main(string[] args)
        {
            for (var i = 1; i <= 4; i++)
            {
                Console.WriteLine(doLevel2($"level2-{i}.txt"));
            }

            Console.Read();
        }

        static int doLevel2(string fileName)
        {
            var data = Utils.Read(Path.Combine(LevelPath, fileName));

            List<Location> locations = data.Item1;
            Tuple<Location, Location> journey = data.Item2;
            Tuple<Location, Location> connection = data.Item3;

            Location closestStart = Location.ClosestTo(journey.Item1,
                new List<Location> {connection.Item1, connection.Item2});
            Location closestEnd = closestStart == connection.Item1 ? connection.Item2 : connection.Item1;

            double walktime1 = journey.Item1.TravelTimeTo(closestStart, Location.SpeedWalk);
            double traveltime = closestStart.TravelTimeToWithStops(closestEnd, Location.SpeedHyperloop);
            double walktime2 = closestEnd.TravelTimeTo(journey.Item2, Location.SpeedWalk);

            int result = Utils.RoundNearest(walktime1 + walktime2 + traveltime);

            return result;
        }
    }
}
