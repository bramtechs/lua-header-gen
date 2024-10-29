namespace LuaHeaderGenLib;

public class BindingBuilderException : Exception
{
    public BindingBuilderException(string message, Exception e) : base(message, e)
    {
    }

    public BindingBuilderException(string message) : base(message)
    {
    }
}