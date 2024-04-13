using System;
using System.Runtime;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

internal class Program
{
    private static int Main(string[] args)
    {
        var idxDoubleDash = Array.IndexOf(args, "--");
        var myArgs = idxDoubleDash < 0
            ? new string[0]
            : args[..idxDoubleDash];
        var thierArgs = idxDoubleDash < 0
            ? args
            : args[(idxDoubleDash+1)..];

        var verbose = myArgs.Contains("-v");
        void Log(string txt)
        {
            if (verbose) Console.WriteLine(txt);
        }

        Log($"args: {string.Join(",", args)}");
        Log($"My args: {string.Join(",", myArgs)}");
        Log($"Thier args: {string.Join(",", thierArgs)}");

        var stdIn = new List<string>();
        while(Console.ReadLine() is {} line)
        {
            stdIn.Add(line);
        }

        var resultArgs = new List<string>();
        foreach(var arg in thierArgs)
        {
            if (arg == "_")
            {
                resultArgs.AddRange(stdIn);
            }
            else
            {
                var inner = arg.Replace("__", string.Join(" ", stdIn))
                    .Replace("_,", string.Join(",", stdIn))
                    .Replace("_+", string.Join("+", stdIn))
                    .Replace("_=", string.Join("=", stdIn));
                resultArgs.Add(inner);
            }
        }

        for(var x=0; x<resultArgs.Count; x++)
        {
            Log($"Out Arg {x}={resultArgs[x]}");
        }

        return ProcessRunInlineConsole(resultArgs.First(), resultArgs.Skip(1).ToArray());
    }

    public static int ProcessRunInlineConsole(string execPath, string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        var proc = Process.Start(execPath, args);
        proc.WaitForExit();
        return proc.ExitCode;
    }
}
