using LuaHeaderGenLib.Domain;

namespace LuaHeaderGenLib;

public class BindingTranslator(Binding binding)
{
    public string Translate()
    {
        string code = DocumentComment();
        if (!string.IsNullOrWhiteSpace(code))
            code += "\n";
        DocumentParameters().ToList().ForEach(p => code += $"{p}\n");
        code += DocumentReturn(binding.ReturnType) + "\n";
        code += TranslateFunction();
        return code;
    }

    private IEnumerable<string> DocumentParameters()
    {
        return binding.Arguments.Select(arg => DocumentParameter(arg.Item1, arg.Item2));
    }

    public string TranslateFunction()
    {
        return TranslateFunction(binding.Name, binding.Arguments);
    }

    public string DocumentComment()
    {
        return DocumentComment(binding.Comment);
    }

    // Static methods

    public static string TranslateFunction(string name, IEnumerable<(string, string)> parameters)
    {
        return $"function {name}({string.Join(", ", parameters.Select(arg => arg.Item2))})\nend";
    }

    public static string DocumentParameter(string type, string name)
    {
        return $"--- @param {name} {TranslateType(type)}";
    }

    public static string DocumentComment(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
            return "";
        return $"--- {comment}";
    }

    public static string DocumentReturn(string returnType)
    {
        return $"--- @return {TranslateType(returnType)}";
    }

    public static string TranslateType(string type)
    {
        return type switch
        {
            "int" or "float" or "double" => "number",
            "bool" => "boolean",
            "std::string" or "char*" or "char *" => "string",
            "void" => "nil",
            _ => type.Replace("*", ""),
        };
    }
}