using System.CommandLine;
using System.CommandLine.Parsing;

public static class Program
{
    public static int Main(string[] args)
    {
        Option<DirectoryInfo> scanDir = new("--dir")
        {
            Description = "Directory to scan for all **/*.cs"
        };

        Option<bool> whiteSpace = new("--whitespace");
        Option<bool> test = new("--test");

        RootCommand rootCommand = new("Quickformat .cs files");
        rootCommand.Options.Add(scanDir);
        rootCommand.Options.Add(whiteSpace);
        rootCommand.Options.Add(test);

        ParseResult parseResult = rootCommand.Parse(args);
        if (parseResult.Errors.Count == 0)
        {
            if (parseResult.GetValue(whiteSpace) is bool ws && ws)
            {
                var argTest = parseResult.GetValue(test);
                if (parseResult.GetValue(scanDir) is DirectoryInfo chDir)
                {
                    ScanFiles.ProcessInputAsFiles(chDir.FullName, argTest);
                }
                else
                {
                    ScanFiles.ProcessInputAsFiles("./", argTest);
                }
                
                return 0;
            }

            Console.Error.WriteLine("No command specified");
            return 2;
        }
        foreach (ParseError parseError in parseResult.Errors)
        {
            Console.Error.WriteLine(parseError.Message);
        }
        return 1;

    }
}

public static class ScanFiles
{
    internal static void ProcessInputAsFiles(string dir, bool test)
    {
        foreach(var file in Directory.EnumerateFiles(dir, "*.cs", SearchOption.AllDirectories))
        {
            if (file.Contains(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar)) continue;
            if (file.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar)) continue;
            ProcessCSharpFile(file, test);
        }
        // while(Console.ReadLine() is string line)
        // {
        //     if (File.Exists(line) && Path.GetExtension(line) == ".cs")
        //     {
        //         ProcessCSharpFile(line, test);
        //     }
        // }
    }

    private static void ProcessCSharpFile(string file, bool test)
    {
        var lines = File.ReadAllLines(file).ToList();
        var dirty = false;
        for(int cc=0; cc<lines.Count; cc++)
        {
            var ln = lines[cc];
            if (ln.Length > 0 && char.IsWhiteSpace(ln[^1]))
            {
                lines[cc] = ln = ln.TrimEnd();
                dirty = true;
            }
            if (ln.Length == 0)
            {
                var nn = cc+1;
                while (nn < lines.Count && string.IsNullOrWhiteSpace(lines[nn]))
                {
                    lines.RemoveAt(nn);
                    dirty = true;
                }
            }
        }
        if (dirty)
        {
            if (test)
            {
                Console.WriteLine(file);
                foreach(var ln in lines)
                {
                    Console.WriteLine($"|{ln}$");
                }
            }
            else
            {
                Console.WriteLine($"Update: {file}");
                File.WriteAllLines(file, lines);
            }
        }
    }
}
