namespace LuaHeaderGenLib;

public class Binding
{
    public readonly string Name;
    public readonly string ReturnType;
    public readonly List<Tuple<string, string>> Arguments = [];

    public Binding(string line)
    {
        var segments = line.Split(" ");
        ReturnType = segments[0];
        Name = segments[1].Split("(")[0];
    }
}
