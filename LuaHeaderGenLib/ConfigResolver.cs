using LuaHeaderGenLib.Domain;

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
        void AddFileIfAllowed(string path, HashSet<string> into)
        {
            var ext = Path.GetExtension(path) ?? string.Empty;
            if (extensions.Contains(ext))
            {
                into.Add(path);
            }
        }

        var files = new HashSet<string>();
        foreach (var fileOrDirectory in filesOrDirectories)
        {
            if (Directory.Exists(fileOrDirectory))
            {
                foreach (var file in Directory.EnumerateFiles(fileOrDirectory, "*", SearchOption.AllDirectories))
                {
                    AddFileIfAllowed(file, files);
                }
            }
            else if (File.Exists(fileOrDirectory))
            {
                AddFileIfAllowed(fileOrDirectory, files);
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
        config.Extensions = SanitizeExtensions(config.Extensions).ToArray();
        config.FilesOrDirectories = [.. CollectFiles(config.FilesOrDirectories, [.. config.Extensions])];
        return config;
    }
}

