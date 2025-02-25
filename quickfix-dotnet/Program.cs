﻿using System;
using System.IO;
using System.Linq;

class Program
{
    public static int Main(string[] args)
    {
        var rel = args.Contains("-rel");
        var list = new List<string>();
        while(Console.ReadLine() is {} line)
        {
            if (ProcessLine(rel, line) is {} match)
            {
                list.Add(match);
            }
        }
        foreach(var line in list.Distinct().OrderBy(x=>x))
        {
            Console.WriteLine(line);
        }
        return 0;
    }

    // C:\git\guy\PiggyBack\src\PiggyBack.Launcher\Runner\ProcessRunnerClipboard.cs(15,39): error CS0535: 'ProcessRunnerClipboard' does not implement interface member 'IProcessRunner.Validate(RunnerCommandArg)' [C:\git\guy\PiggyBack\src\PiggyBack.Launcher\PiggyBack.Launcher.csproj]
    public static string? ProcessLine(bool rel, string line)
    {
        int idxPrenOpen = line.IndexOf('(');
        if (idxPrenOpen < 0) return null;
        int idxComma = line.IndexOf(',', idxPrenOpen);
        if (idxComma < 0) return null;
        int idxPrenClose = line.IndexOf(')', idxComma);
        if (idxPrenClose < 0) return null;

        var path = line[..idxPrenOpen];
        var lineNo = line[(idxPrenOpen+1)..idxComma];
        var col = line[(idxComma+1)..idxPrenClose];
        var content = CleanContent( line[(idxPrenClose+2)..] );

        var embelish = Embellish(path, lineNo, col, content);
        var polishPath = PolishPath(path, lineNo, col, content, rel);

        return $"{path}:{lineNo}:{col}: {embelish}";
    }

    private static string PolishPath(string path, string lineNo, string col, string content, bool rel)
    {
        return path;
    }

    private static string Embellish(string path, string lineNo, string col, string content)
    {
        var exists = File.Exists(path);
        return $"{content} {(exists ? "": "(?)")}";
    }

    public static string CleanContent(string txt)
    {
        var idxCollon = txt.IndexOf(':');
        if (idxCollon < 0) return txt;

        var idxLastSqBracket = txt.LastIndexOf('[');
        if (idxLastSqBracket < 0) return txt[( idxCollon+1 )..];
        return txt[( idxCollon+1 ) .. idxLastSqBracket];
    }
}
