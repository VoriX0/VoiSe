using Microsoft.UI.Xaml.Data;
using System;

namespace VoiSe.App;

public sealed class TimeSliderValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double seconds)
        {
            return FormatTime(seconds);
        }

        if (value is float floatSeconds)
        {
            return FormatTime(floatSeconds);
        }

        if (value is int intSeconds)
        {
            return FormatTime(intSeconds);
        }

        return "00:00";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return 0d;
    }

    private static string FormatTime(double seconds)
    {
        if (double.IsNaN(seconds) || seconds < 0)
        {
            seconds = 0;
        }

        var span = TimeSpan.FromSeconds(seconds);
        return span.TotalHours >= 1 ? span.ToString(@"h\:mm\:ss") : span.ToString(@"mm\:ss");
    }
}
