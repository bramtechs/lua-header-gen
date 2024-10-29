using LuaHeaderGenLib;
using Newtonsoft.Json;

namespace LuaHeaderGenCmd
{
    public static class Program
    {
        public class Options
        {
            public string[]? files;
            public string? output;
            public string[]? macros;
            public string[]? extensions;
        }

        public static void StartWithOptions(Options options)
        {
            try
            {
                string outputFile = options.output ?? throw new MissingFieldException("Output file not specified");
                var generator = new LuaHeaderGenerator(new()
                {
                    FilesOrDirectories = options.files ?? [],
                    Extensions = options.extensions ?? [],
                    Macros = options.macros ?? [],
                    OutputFile = outputFile,
                });
                string code = generator.GenerateLuaHeaderCode();
                File.WriteAllText(outputFile, code);

                string absoluteOutputFile = Path.GetFullPath(outputFile);
                Console.WriteLine($"Generated Lua header code to {absoluteOutputFile}");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("An error occured with LuaHeaderGen:");
                Console.Error.WriteLine($"Error: {e}");
            }
            Environment.Exit(0);
        }

        public static void NormalMain(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: LuaHeaderGenCmd.exe <path to json file>");
                Environment.Exit(1);
            }

            try
            {
                var jsonFile = args[^1] ?? throw new Exception("No JSON file specified");
                var jsonText = File.ReadAllText(jsonFile);
                var options = JsonConvert.DeserializeObject<Options>(jsonText) ?? throw new Exception("Failed to deserialize JSON");

                // migrate to new config
                string configDir = Path.GetDirectoryName(jsonFile) ?? throw new Exception("Could not determine directory of json config");
                Directory.SetCurrentDirectory(configDir);

                StartWithOptions(options);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"JSON config load error: {e.Message}");
            }
        }

        public static void Main(string[] args)
        {
            if (false)
            {
                NormalMain(args);
            }
            else
            {
                NormalMain(["C:\\dev-new\\howling\\lua_header_gen.json"]);
            }
        }
    }
}