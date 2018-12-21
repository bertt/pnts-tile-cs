using Pnts.Tile;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string infile = "testfixtures/1-0-1-1.pnts";
            var stream = File.OpenRead(infile);
            Console.WriteLine("Pnts tile sample application");
            Console.WriteLine($"Start parsing {infile}...");
            var pnts = PntsParser.ParsePnts(stream);
            Console.WriteLine($"Number of points: {pnts.FeatureTableMetadata.points_length} ");
            var rtc = pnts.FeatureTableMetadata.Rtc_Center;
            Console.WriteLine($"RTC_CENTER (relative to center x,y,z): {rtc[0]},{rtc[1]},{rtc[2]}");
            Console.WriteLine($"First point (x,y,z): {pnts.Points[0].X}, {pnts.Points[0].Y}, {pnts.Points[0].Z} ");
            Console.WriteLine($"First point color (r,g,b): {pnts.Colors[0].R}, {pnts.Colors[0].G}, {pnts.Colors[0].B} ");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
