using CommandLine;
using LuaHeaderGenLib;

namespace LuaHeaderGenCmd
{
    public static class Program
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

        public static void StartWithOptions(Options options)
        {
            try
            {
                var generator = new LuaHeaderGenerator(new()
                {
                    FilesOrDirectories = options.Files,
                    Extensions = options.Extensions,
                    OutputFile = options.Output,
                });
                generator.GenerateLuaHeaderCode();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("An error occured with LuaHeaderGen:");
                Console.Error.WriteLine($"Error: {e}");
            }
            Environment.Exit(0);
        }

        public static void HandleArgumentError(IEnumerable<Error> errors)
        {
            Console.Error.WriteLine("Failed to run LuaHeaderGen:");
            foreach (var error in errors)
            {
                Console.Error.WriteLine(error);
            }
            Environment.Exit(1);
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("No arguments passed");
                return;
            }

            Parser.Default.Settings.AutoHelp = true;
            Parser.Default.ParseArguments<Options>(args)
                       .WithParsed(StartWithOptions)
                       .WithNotParsed(HandleArgumentError);
        }
    }
}