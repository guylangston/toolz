//Sample
// diff --git a/src/Website.BackEnd/SignalR/Hubs/RfsHubImpl.cs b/src/Website.BackEnd/SignalR/Hubs/RfsHubImpl.cs
// @@ -35,8 +35,6 @@ using Microsoft.AspNetCore.SignalR;
// diff --git a/src/Website.BackEnd/SignalR/Impl/CancellableItemCache.cs b/src/Website.BackEnd/SignalR/Impl/CancellableItemCache.cs
// @@ -28,7 +28,6 @@ using System;
// @@ -60,9 +59,12 @@ public class CancellableItemCache<TKey, TValue>
// @@ -187,4 +189,4 @@ public class CancellableItemCache<TKey, TValue>
//

var argOne = args.Length > 0 && args.Any(x=>x == "-1");

var lastDiff = "";
var file = "";
var ln = 0;
var fileDiffs = new List<DiffChunk>();
while( Console.ReadLine() is {} line) 
{
    ln++;
    try
    {
        if (line.StartsWith("diff "))
        {
            PushFileDiffs();
            lastDiff = line;
            var parts = line.Split(' ');
            file = parts[2].Remove(0, 2);
        }
        else if (line.StartsWith("@@ "))
        {
            var split = line.IndexOf("@@", 4);
            var prefix = line[3..split];
            var psp = prefix.Trim().Split(' ');
            var ppsp = psp[1].Split(',');
            var cln = int.Parse(ppsp[0]);
            var size = int.Parse(ppsp[1]);

            var summary = line[(split+2)..];
            fileDiffs.Add(new DiffChunk(file, cln, size, summary));
        }
    }
    catch(Exception ex)
    {
        Console.Error.WriteLine($"Line: {ln} | {line}");
        Console.Error.WriteLine(ex.ToString());
    }
}
PushFileDiffs();
return 0;

void PushFileDiffs()
{
    if (fileDiffs.Any()) 
    {
        if (argOne)
        {
            var count = fileDiffs.Count;
            Console.WriteLine(fileDiffs.MaxBy(x=>x.size).ToString(count > 1 ? $"[++{count}]" : ""));
        }
        else {
            foreach(var line in fileDiffs)
            {
                Console.WriteLine(line.ToString());
            }
        }
        fileDiffs.Clear();
    }
}

record DiffChunk(string file, int line, int size, string summary)
{
    public override string ToString() => $"{file}|{line}| [{size}] {summary}";
    public string ToString(string prefix) => $"{file}|{line}| {prefix} [{size}] {summary}";
};

