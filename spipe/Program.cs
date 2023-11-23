using System;
using System.Linq;
using System.Collections.Generic;

internal class Program
{
    // Format all around the largest instance of `split`
    static void Cols(string split)
    {
        var all = new List<string>();
        while (Console.ReadLine() is { } ln)
        {
            all.Add(ln);
        }
        var max = all.Max(x => x.IndexOf(split));
        if (max >= 0)
        {
            foreach (var ln in all)
            {
                var idx = ln.IndexOf(split);
                if (idx < 0)
                {
                    Console.WriteLine(ln);
                }
                else
                {
                    Console.Write(ln[0..idx].PadRight(max));
                    Console.Write(split);
                    Console.WriteLine(ln[(idx + split.Length)..]);
                }
            }
        }
    }

    static void JsUsingsHackSort()
    {
        bool FindFrom(string line, out string path) 
        {
            var idx = line.IndexOf("from");
            if (idx < 0)
            {
                path = "";
                return false;
            }
            path = line[(idx + 5)..];
            return true;
        }

        var all = new List<string>();
        var parts = new List<(string ln, string path)>();
        while (Console.ReadLine() is { } ln)
        {
            all.Add(ln);
            if (FindFrom(ln, out var path)) {
                parts.Add((ln, path));
            }
            else {
                Console.WriteLine(ln);
            }
        }

        foreach(var import in parts.OrderBy(x=>x.path.Length).ThenBy(x=>x.path))
        {
            Console.WriteLine(import.ln);
        }
    }

    private static void Main(string[] args)
    {
        if (args.Length > 1 && args[0] == "cols")
        {
            Cols(args[1]);
            return;
        }
        if (args.Length > 0 && args[0] == "js-import")
        {
            JsUsingsHackSort();
            return;
        }

        Console.Error.WriteLine("unknown command: expected cols, js-import");
    }
}
