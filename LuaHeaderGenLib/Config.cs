namespace LuaHeaderGenLib;
public class Config
{
    public string[] Files { get; set; } = [];
    public string[] RawFileContents { get; set; } = [];
    public string[] LuaMacros = ["LUAFUNC", "RLAPI"];
    public string OutputFile { get; set; }
}
