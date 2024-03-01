using System.Diagnostics;

public static class Program
{
    // Try $env:shimexec = "nvim"; $env:shima = "--clean" ; ls | vim-shim.exe --
    public static int Main(string[] args)
    {
        var shimExec = Environment.GetEnvironmentVariable("ShimExec");
        if (shimExec == null)
        {
            Console.Error.WriteLine("Env:ShimExec missing");
            return -1;
        }

        // TODO: Snip -> Console.WriteLine($"");
        Console.WriteLine($" isIn: {Console.IsInputRedirected}");
        Console.WriteLine($"isOut: {Console.IsOutputRedirected}");
        Console.WriteLine($"isErr: {Console.IsErrorRedirected}");

        var innerArgs = new List<string> { };
        foreach(string key in Environment.GetEnvironmentVariables().Keys)
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
                        // https://en.wikipedia.org/wiki/End-of-Transmission_character
                        // if (line.Length == 1 && line[0] == 4 /* ^D */) break;
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
