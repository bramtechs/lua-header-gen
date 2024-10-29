﻿namespace LuaHeaderGenLib;

internal class GeneratorException : Exception
{
    public GeneratorException(string message, Exception e) : base(message)
    {
    }

    public GeneratorException(string message) : base(message)
    {
    }
}