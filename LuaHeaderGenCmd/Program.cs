using CommandLine;
using LuaHeaderGenLib;

namespace LuaHeaderGenCmd
{
    public class Program
    {
        public class Options
        {
            [Option('f', "files", Required = true, HelpText = "Files or directories")]
            public string[] Files { get; set; }

            [Option('e', "extensions", Required = false, HelpText = "Extensions filter")]
            public string[] Extensions { get; set; }

            [Option('o', "output", Required = true, HelpText = "Lua file to output")]
            public string Output { get; set; }
        }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                       .WithParsed(options =>
                       {
                           var generator = new LuaHeaderGenerator(new()
                           {
                               FilesOrDirectories = options.Files,
                               Extensions = options.Extensions,
                               OutputFile = options.Output,
                           });

                           generator.GenerateLuaHeaderCode();
                       });

        }
    }
}