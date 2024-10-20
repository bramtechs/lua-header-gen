using LuaHeaderGenLib;

namespace LuaHeaderGenCmd;
internal class Program
{
    static void Main(string[] args)
    {
        var generator = new LuaHeaderGenerator(new()
        {
            Files = ["C:\\dev\\breakout\\core\\breakout.hh"],
            RawFileContents = ["test"],
            OutputFile = "output.h"
        });

        generator.GenerateLuaHeaderCode();
    }
}

