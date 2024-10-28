using System.Text.RegularExpressions;

namespace LuaHeaderGenLib;

public static partial class StringUtils
{
    public static string RemoveDuplicateSpacing(string line)
    {
        return SpaceReducerRegex().Replace(line, " ");
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex SpaceReducerRegex();

    public static string TrimMultiline(string line)
    {
        return string.Join("\n", line.Split('\n').Select(l => l.Trim()));
    }
}