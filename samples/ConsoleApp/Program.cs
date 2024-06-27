using DotSpatial.Positioning;
using Pnts.Tile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp;

class Program
{
    
    static void Main(string[] args)
    {
        string infile = "testfixtures/1-0-1-1.pnts";
        var stream = File.OpenRead(infile);
        Console.WriteLine("Pnts tile sample application");
        Console.WriteLine($"Start parsing {infile}...");
        var pnts = PntsSerializer.Deserialize(stream);

        var rtc = pnts.FeatureTableMetadata.Rtc_Center;
        var rtc_cartesian = GetCartesianPoint(rtc[0], rtc[1], rtc[2]);
        var cartesian_points = GetCartesianPoints(pnts);
        var wgs84Points = GetWgs84Points(cartesian_points, rtc_cartesian);
        WritePositions3DToCsv("positions.csv", wgs84Points);

        var first_point = wgs84Points[0];
        Console.WriteLine($"Number of points: {pnts.FeatureTableMetadata.points_length} ");
        Console.WriteLine($"RTC_CENTER (relative to center x,y,z): {rtc[0]},{rtc[1]},{rtc[2]}");
        Console.WriteLine($"First point (x,y,z): {pnts.Points[0].X}, {pnts.Points[0].Y}, {pnts.Points[0].Z} ");
        Console.WriteLine($"First point (longitude, latitude, altitude): {first_point.Longitude.DecimalDegrees}, {first_point.Latitude.DecimalDegrees}, {first_point.Altitude} ");
        Console.WriteLine($"First point color (r,g,b): {pnts.Colors[0].R}, {pnts.Colors[0].G}, {pnts.Colors[0].B} ");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }


    private static void WritePositions3DToCsv(string file, List<Position3D> points)
    {
        var csv = new StringBuilder();
        foreach (var p in points)
        {
            var newLine = $"{p.Longitude.DecimalDegrees},{p.Latitude.DecimalDegrees},{p.Altitude.Value}";
            csv.AppendLine(newLine);
        }
        File.WriteAllText(file, csv.ToString());
    }


    private static List<Position3D> GetWgs84Points(List<CartesianPoint> points, CartesianPoint rtc)
    {
        var result = new List<Position3D>();

        foreach(var point in points)
        {
            result.Add((rtc + point).ToPosition3D());
        }
        return result;
    }

    private static List<CartesianPoint> GetCartesianPoints(Pnts.Tile.Pnts pnts)
    {
        var cartesianpoints = new List<CartesianPoint>();
        foreach (var p in pnts.Points)
        {
            var cartesian_point = GetCartesianPoint(p.X, p.Y, p.Z);
            cartesianpoints.Add(cartesian_point);
        }
        return cartesianpoints;
    }

    private static CartesianPoint GetCartesianPoint(float x, float y, float z)
    {
        var distance_x = new Distance(x, DistanceUnit.Meters);
        var distance_y = new Distance(y, DistanceUnit.Meters);
        var distance_z = new Distance(z, DistanceUnit.Meters);

        var cartesian = new CartesianPoint(distance_x, distance_y, distance_z);
        return cartesian;
    }
}
