using System;
using System.Runtime;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

internal static class Program
{
    private static int Main(string[] args)
    {
        var timer = new Stopwatch();
        timer.Start();
        var ret = ProcessRunInlineConsole(args.First(), args.Skip(1).ToArray());
        timer.Stop();
        Console.WriteLine($"Completed: {timer.Elapsed} -- {Humanize(timer.Elapsed)}");
        return ret;
    }

    public static string Humanize(TimeSpan span)
    {
        if (span.TotalSeconds < 1) return $"{span.Milliseconds} ms";
        if (span.TotalMinutes < 1) return $"{span.Seconds} sec, {span.Milliseconds} ms";
        if (span.TotalHours < 1)   return $"{span.Minutes} min, {span.Seconds} sec";
        if (span.TotalDays < 1)    return $"{span.Hours} hr, {span.Minutes} min";
        if (span.TotalDays > 365)  return $"{(int)span.TotalDays / 365} yrs, {(int)span.TotalDays % 365} days";

        if (span.Hours == 0) return $"{span.Days} days";
        return $"{span.Days} days, {span.Hours} hr";
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
