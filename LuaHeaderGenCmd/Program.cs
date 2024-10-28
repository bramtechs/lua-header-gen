using LuaHeaderGenLib;

var generator = new LuaHeaderGenerator(new()
{
    Files = ["C:\\dev\\breakout\\core\\breakout.hh"],
    RawFileContents = ["test"],
    OutputFile = "output.h"
});

generator.GenerateLuaHeaderCode();