# pnts-tile-cs

Library for (de)serializing  LiDAR  tiles

[![NuGet Status](http://img.shields.io/nuget/v/pnts-tile.svg?style=flat)](https://www.nuget.org/packages/pnts-tile/)

Spec: https://github.com/AnalyticalGraphicsInc/3d-tiles/tree/master/specification/TileFormats/PointCloud

## Sample code

```
string infile = "testfixtures/1-0-1-1.pnts";
var stream = File.OpenRead(infile);
var pnts = PntsParser.ParsePnts(stream);
Console.WriteLine($"Number of points: {pnts.Points.Count} ");
Console.WriteLine($"First point (x,y,z): {pnts.Points[0].X}, {pnts.Points[0].Y}, {pnts.Points[0].Z} ");
```
