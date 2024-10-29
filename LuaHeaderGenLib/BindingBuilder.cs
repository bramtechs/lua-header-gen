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

        if (returnType.Contains('('))
        {
            throw new BindingBuilderException($"Return type is invalid, syntax error in line '{line}'?");
        }

        List<(string, string)> arguments = [];

        void ParseParameter(string parameter)
        {
            string param = parameter.Trim();

            // dont care about variadic arguments
            if (param == "...")
                return;

            // const is irrelevant
            param = parameter.Replace("const", "");

            // stick * to the type
            param = param.Replace(" *", "* ");

            param = StringUtils.RemoveDuplicateSpacing(param).Trim();

            string[] typeAndName = param.Split(" ");
            arguments.Add((typeAndName[0], typeAndName[1]));
        }

        string parameterString = line[line.IndexOf('(')..(line.IndexOf(')') + 1)];
        string parameterStringNoParens = parameterString[1..(parameterString.Length - 1)];

        // void means no parameters
        parameterStringNoParens = parameterStringNoParens.Replace("void", "");

        if (parameterStringNoParens.Replace(" ", "").Length > 0)
        {
            if (parameterStringNoParens.Contains(',')) // multiple parameters
            {
                string[] parameters = parameterStringNoParens.Split(',');
                foreach (string parameter in parameters)
                {
                    ParseParameter(parameter);
                }
            }
            else // single parameter
            {
                ParseParameter(parameterStringNoParens);
            }
        }

        // check for comments
        string comment = string.Empty;

        int commentIndex = line.IndexOf("//");
        if (commentIndex > 0)
        {
            comment = line[(commentIndex + 2)..].Trim();
        }

        return new Binding(name, returnType, arguments, comment);
    }
}