# pnts-tile-cs

Library for (de)serializing  LiDAR  tiles

[![NuGet Status](http://img.shields.io/nuget/v/pnts-tile.svg?style=flat)](https://www.nuget.org/packages/pnts-tile/)

Spec: https://github.com/AnalyticalGraphicsInc/3d-tiles/tree/master/specification/TileFormats/PointCloud

3D Tiles Candidate OGC® Community Standard (pdf alert): https://portal.opengeospatial.org/files/79137

## Sample code

```
string infile = "testfixtures/1-0-1-1.pnts";
var stream = File.OpenRead(infile);
var pnts = PntsParser.ParsePnts(stream);
Console.WriteLine($"Number of points: {pnts.FeatureTableMetadata.points_length} ");
var rtc = pnts.FeatureTableMetadata.Rtc_Center;
Console.WriteLine($"RTC_CENTER (relative to center x,y,z): {rtc[0]},{rtc[1]},{rtc[2]}");
Console.WriteLine($"First point (x,y,z): {pnts.Points[0].X}, {pnts.Points[0].Y}, {pnts.Points[0].Z} ");
Console.WriteLine($"First point color (r,g,b): {pnts.Colors[0].R}, {pnts.Colors[0].G}, {pnts.Colors[0].B} ");
```
