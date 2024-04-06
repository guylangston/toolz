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
        innerArgs.AddRange(args);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
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
            innerArgs.Add(tmp);  // last arg in the temp file
        }
        var proc = Process.Start(shimExec, innerArgs.ToArray());
        proc.WaitForExit();
        return proc.ExitCode;
    }
}
