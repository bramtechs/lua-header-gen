namespace LuaHeaderGenLib.Domain;

public class Config
{
    public string[] FilesOrDirectories { get; set; } = [];
    public string[] Extensions { get; set; } = [];
    public string[] RawFileContents { get; set; } = [];
    public string[] Macros = ["LUAFUNC", "RLAPI"];
    public string OutputFile { get; set; }
}