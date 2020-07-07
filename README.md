# pnts-tile-cs

.NET Standard 2.1 Library for (de)serializing  Cesium pnts tiles

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

See https://github.com/bertt/pnts-tile-cs/blob/master/samples/ConsoleApp/Program.cs for sample code converting points in tile to longitude, latitude, altitude and writing to csv file.

## Positions

From the spec: "RTC_CENTER specifies the center position and all point positions are treated as relative to this value". 
So for each point, add the point (x,y,z) to the RTC_CENTER to get its position in Cartesian format. 
For sample code calculating to longitude, latitude, altitude see https://github.com/bertt/pnts-tile-cs/blob/master/samples/ConsoleApp/Program.cs#L19

## Dependencies

- System.Text.Json

## Known limitis

- add support for POSITION_QUANTIZED, RGBA, RGB565, NORMAL, NORMAL_OCT16P, BATCH_ID, QUANTIZED_VOLUME_OFFSET, QUANTIZED_VOLUME_SCALE, CONSTANT_RGBA,BATCH_LENGTH

- add pnts writer functionality


## History

2018-12-19: Initial coding - pnts reader

