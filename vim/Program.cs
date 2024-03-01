using System.Diagnostics;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public static class Program
{
    public static int Main(string[] args)
    {
        var shimExec = "nvim.exe";
        var innerArgs = new List<string> { "--clean" };
        foreach(string key in Environment.GetEnvironmentVariables().Keys.Cast<string>().OrderBy(x=>x))
        {
            if (string.Equals(key, "ShimExe", StringComparison.OrdinalIgnoreCase)) continue;
            if (key.ToLower().StartsWith("shim"))
            {
                var val = Environment.GetEnvironmentVariable(key.ToString());
                if (val != null)
                {
                    Console.WriteLine($"ShimArg: {key}={val}");
                    innerArgs.Add(val);
                }
            }
        }
        foreach(var arg in args)
        {
            Console.WriteLine($"Arg: {arg}");
            innerArgs.Add(arg);
        }

        if (Console.IsInputRedirected)
        {
            var tmp = Path.GetTempFileName();
            var count = 0;
            using(var tw = File.OpenWrite(tmp))
            {
                using(var sw = new StreamWriter(tw))
                {
                    while(Console.ReadLine() is {} line)
                    {
                        sw.WriteLine(line);
                        count++;
                    }
                }
            }
            innerArgs.Add(tmp);
        }

        var proc = Process.Start(shimExec, innerArgs.ToArray());
        proc.WaitForExit();
        return proc.ExitCode;
    }
}
