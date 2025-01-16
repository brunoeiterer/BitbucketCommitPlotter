using System.Text.Json;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;

namespace BitbucketCommitPlotter.Models;

public class CommitData : SortedDictionary<DateTime, int>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    public static CommitData FromJson(string json) =>
        JsonSerializer.Deserialize<CommitData>(json) ?? throw new JsonException("Could not parse the provided json");

    public string ToJson() => JsonSerializer.Serialize(this, JsonSerializerOptions);

    public void PlotCommitGraph(DateTime startDate, DateTime endDate, string name)
    {
        var totalDays = (endDate - startDate).Days + 1;

        var heatmapData = new double[53, 7];
        foreach (var entry in this.Where(kvp => kvp.Key >= startDate && kvp.Key <= endDate))
        {
            var dayOfYear = (entry.Key - startDate.AddDays(-(int)startDate.DayOfWeek)).Days;
            var week = dayOfYear / 7;
            var dayOfWeek = 6 - (int)entry.Key.DayOfWeek;
            heatmapData[week, dayOfWeek] = entry.Value;
        }

        var plotModel = new PlotModel
        {
            Title = $"Commit Activity - {name} - {startDate.Year}"
        };

        var heatMapSeries = new HeatMapSeries
        {
            X0 = 0,
            X1 = 52,
            Y0 = 0,
            Y1 = 6,
            Interpolate = false,
            RenderMethod = HeatMapRenderMethod.Rectangles,
            Data = heatmapData
        };

        plotModel.Series.Add(heatMapSeries);

        var xAxis = new LinearAxis
        {
            IsAxisVisible = false
        };

        var yAxis = new CategoryAxis
        {
            IsAxisVisible = false
        };

        var colorAxis = new LinearColorAxis
        {
            Position = AxisPosition.Right,
            Palette = new OxyPalette()
            {
                Colors = [
                    OxyColor.FromRgb(0xcd, 0xcd, 0xcd),
                    OxyColor.FromRgb(0xd6, 0xee, 0xd8),
                    OxyColor.FromRgb(0xc1, 0xe7, 0xc4),
                    OxyColor.FromRgb(0xac, 0xdf, 0xaf),
                    OxyColor.FromRgb(0x97, 0xd7, 0x9b),
                    OxyColor.FromRgb(0x82, 0xcf, 0x87),
                    OxyColor.FromRgb(0x6d, 0xc6, 0x73),
                    OxyColor.FromRgb(0x58, 0xbe, 0x5f),
                    OxyColor.FromRgb(0x43, 0xb6, 0x4b),
                    OxyColor.FromRgb(0x2e, 0xae, 0x37)
                ]
            },
            Minimum = 0,
            Maximum = heatmapData.Cast<double>().Max(),
            IsAxisVisible = false
        };

        plotModel.Axes.Add(xAxis);
        plotModel.Axes.Add(yAxis);
        plotModel.Axes.Add(colorAxis);

        for (var week = 0; week < 53; week++)
        {
            for (int day = 0; day < 7; day++)
            {
                var rect = new RectangleAnnotation
                {
                    MinimumX = week - 0.5,
                    MaximumX = week + 0.5,
                    MinimumY = day - 0.5,
                    MaximumY = day + 0.5,
                    Stroke = OxyColors.White,
                    StrokeThickness = 1,
                    Fill = OxyColors.Transparent
                };
                plotModel.Annotations.Add(rect);
            }
        }

        using var stream = File.Create($"CommitData-{name}-{startDate.Year}.png");
        var pngExporter = new PngExporter { Width = 1200, Height = 200 };
        pngExporter.Export(plotModel, stream);
    }
}