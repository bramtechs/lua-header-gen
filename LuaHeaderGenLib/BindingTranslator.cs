using LuaHeaderGenLib.Domain;

namespace LuaHeaderGenLib;

public class BindingTranslator(Binding binding)
{
    public string Translate()
    {
        return string.Join("\n", DocumentParameters().Concat([TranslateFunction()]));
    }

    private IEnumerable<string> DocumentParameters()
    {
        return binding.GetArguments().Select(arg => DocumentParameter(arg.Item1, arg.Item2));
    }

    public string TranslateFunction()
    {
        return TranslateFunction(binding.Name, binding.GetArguments());
    }

    public static string TranslateFunction(string name, IEnumerable<(string, string)> parameters)
    {
        return $"function {name}({string.Join(", ", parameters.Select(arg => arg.Item2))})\nend";
    }

    public static string DocumentParameter(string type, string name)
    {
        return $"--- @param {name} {TranslateType(type)}";
    }

    public static string TranslateType(string type)
    {
        return type switch
        {
            "int" or "float" or "double" => "number",
            "bool" => "boolean",
            "std::string" or "const char*" or "const char *" => "string",
            _ => type,
        };
    }
}