using System;
using System.Linq;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length > 1 && args[0] == "cols")
        {
            var split = args[1];
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
    }
}
