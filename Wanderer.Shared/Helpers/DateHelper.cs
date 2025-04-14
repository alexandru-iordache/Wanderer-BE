namespace Wanderer.Shared.Helpers;

public static class DateHelper
{
    public static string SubstringDateOnly(string date)
    {
        return date.Substring(0, 10);
    }
}
