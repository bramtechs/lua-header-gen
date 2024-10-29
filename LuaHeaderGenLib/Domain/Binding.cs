namespace LuaHeaderGenLib.Domain;

public class Binding
{
    public string Name { get; private set; }
    public string ReturnType { get; private set; }
    public List<(string, string)> Arguments { get; private set; }

    public string Comment { get; private set; }

    public Binding(string name, string returnType, List<(string, string)> arguments, string comment)
    {
        Name = name;
        ReturnType = returnType;
        Arguments = arguments;
        Comment = comment;
    }

    public (string, string) GetArgument(string name) => Arguments.First(arg => arg.Item2 == name);

    public string GetArgumentType(string name) => GetArgument(name).Item1;
}