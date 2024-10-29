using LuaHeaderGenLib.Domain;

namespace LuaHeaderGenLib;

public class LuaHeaderGenerator
{
    private readonly Dictionary<string, string> _sources = [];
    private readonly Config _config;

    private readonly HashSet<Binding> _bindings = [];

    public LuaHeaderGenerator(Config config)
    {
        _config = config;
        for (int i = 0; i < config.RawFileContents.Length; i++)
        {
            string rawFileContent = config.RawFileContents[i];
            _sources.Add($"$raw{i}", rawFileContent);
        }

        foreach (string filePath in config.FilesOrDirectories)
        {
            if (!File.Exists(filePath))
                throw new GeneratorException($"File {filePath} does not exist");

            _sources.Add(filePath, File.ReadAllText(filePath));
        }

        if (_sources.Count == 0)
            throw new GeneratorException("No files or raw file contents provided");
    }

    private string? StartsWithMacro(string line)
    {
        foreach (string macro in _config.LuaMacros)
        {
            if (line.StartsWith(macro))
            {
                return macro;
            }
        }
        return null;
    }

    private void ParseFile(string content, string filePath)
    {
        string[] lines = content.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            line = line.Trim();

            var macro = StartsWithMacro(line);
            if (macro == null)
                return;

            line = line[macro.Length..].Trim();

            try
            {
                Binding binding = BindingBuilder.Build(line);
                _bindings.Add(binding);
            }
            catch (Exception e)
            {
                throw new GeneratorException($"Failed to parse line {i} in file {filePath}: {line}", e);
            }

            Console.WriteLine(line);
        }
    }

    public string GenerateLuaHeaderCode()
    {
        foreach (var (filePath, source) in _sources)
        {
            ParseFile(source, filePath);
        }
        return "";
    }
}