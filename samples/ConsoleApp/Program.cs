using Pnts.Tile;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string infile = "testfixtures/1-0-1-1.pnts";

            var stream = File.OpenRead(infile);
            Console.WriteLine("Pnts tile sample application");
            Console.WriteLine($"Start parsing {infile}...");
            var pnts = PntsParser.ParsePnts(stream);
            Console.WriteLine($"Number of points: {pnts.Points.Count} ");
            Console.WriteLine($"First point (x,y,z): {pnts.Points[0].X}, {pnts.Points[0].Y}, {pnts.Points[0].Z} ");
        }
    }
}
