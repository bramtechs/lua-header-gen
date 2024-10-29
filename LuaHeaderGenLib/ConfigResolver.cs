using LuaHeaderGenLib.Domain;
using System.IO;

namespace LuaHeaderGenLib;

public class ConfigResolver(Config config)
{
    public static HashSet<string> SanitizeExtensions(string[] extensions)
    {
        return extensions.Select(x => x.ToLower()).Select(EnsurePrefixDot).ToHashSet();
    }

    public static string EnsurePrefixDot(string x)
    {
        return x.StartsWith('.') ? x : $".{x}";
    }

    public static HashSet<string> CollectFiles(string[] filesOrDirectories, string[] extensions)
    {
        var files = new HashSet<string>();
        foreach (var fileOrDirectory in filesOrDirectories)
        {
            if (Directory.Exists(fileOrDirectory))
            {
                foreach (var file in Directory.EnumerateFiles(fileOrDirectory, "*", SearchOption.AllDirectories))
                {
                    var ext = Path.GetExtension(file) ?? string.Empty;
                    if (extensions.Contains(ext))
                    {
                        files.Add(file);
                    }
                }
            }
            else if (File.Exists(fileOrDirectory))
            {
                files.Add(fileOrDirectory);
            }
            else
            {
                throw new InvalidOperationException($"File or folder {fileOrDirectory} does not exist");
            }
        }

        return files;
    }

    public Config ValidateAndResolve()
    {
        if (config.OutputFile.Length == 0)
        {
            throw new InvalidOperationException("Output file must be specified");
        }

        if (config.Macros.Length == 0)
        {
            throw new InvalidOperationException("At least one macro must be specified");
        }

        config.Extensions = [.. SanitizeExtensions(config.Extensions)];
        config.FilesOrDirectories = [.. CollectFiles(config.FilesOrDirectories, [.. config.Extensions])];
        return config;
    }
}