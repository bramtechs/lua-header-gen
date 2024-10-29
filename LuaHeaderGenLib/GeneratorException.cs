namespace LuaHeaderGenLib;

internal class GeneratorException : Exception
{
    public GeneratorException(string message, Exception e) : base(message, e)
    {
    }

    public GeneratorException(string message) : base(message)
    {
    }
}