using System.Text.RegularExpressions;

namespace LuaHeaderGenLib.Domain;

public class Binding
{
    public string Name { get; private set; }
    public string ReturnType { get; private set; }
    private List<(string, string)> _arguments = [];

    public Binding(string name, string returnType, List<(string, string)> arguments)
    {
        Name = name;
        ReturnType = returnType;
        _arguments = arguments;
    }

    public (string, string) GetArgument(string name) => _arguments.First(arg => arg.Item2 == name);

    public string GetArgumentType(string name) => GetArgument(name).Item1;
}
