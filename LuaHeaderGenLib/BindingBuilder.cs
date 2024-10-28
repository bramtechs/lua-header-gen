using LuaHeaderGenLib.Domain;

namespace LuaHeaderGenLib;

public static partial class BindingBuilder
{
    public static Binding Build(string line)
    {
        line = StringUtils.RemoveDuplicateSpacing(line);

        string[] segments = line.Split(" ");
        string returnType = segments[0];
        string[] splits = segments[1].Split("(");
        string name = splits[0];

        List<(string, string)> arguments = [];
        string parameterString = line[line.IndexOf('(')..(line.IndexOf(')') + 1)];
        if (parameterString.Contains(','))
        {
            string parameterStringNoParens = parameterString[1..(parameterString.Length - 1)];
            string[] parameters = parameterStringNoParens.Split(", ");
            foreach (string parameter in parameters)
            {
                string[] typeAndName = parameter.Trim().Split(" ");
                arguments.Add((typeAndName[0], typeAndName[1]));
            }
        }

        return new Binding(name, returnType, arguments);
    }
}