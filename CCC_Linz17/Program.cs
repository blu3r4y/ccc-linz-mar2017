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
        public static string LevelPath = Path.Combine(DataPath, @"level1\");

        static void Main(string[] args)
        {
            Console.WriteLine("level1-1:" + doLevel1("level1-1.txt"));
            Console.WriteLine("level1-2:" + doLevel1("level1-2.txt"));
            Console.WriteLine("level1-3:" + doLevel1("level1-3.txt"));
            Console.WriteLine("level1-4:" + doLevel1("level1-4.txt"));
            Console.Read();
        }

        static int doLevel1(string fileName)
        {
            var data = InputReader.Read(Path.Combine(LevelPath, fileName));

            Tuple<Location, Location> fromTo = data.Item2;
            int traveltime = fromTo.Item1.TravelTimeToWithoutStops(fromTo.Item2) + 200;

            return traveltime;
        }
    }
}
