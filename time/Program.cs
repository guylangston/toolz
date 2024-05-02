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
        var timer = new Stopwatch();
        timer.Start();
        var ret = ProcessRunInlineConsole(args.First(), args.Skip(1).ToArray());
        timer.Stop();
        Console.WriteLine($"Completed: {timer.Elapsed}");
        return ret;
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
