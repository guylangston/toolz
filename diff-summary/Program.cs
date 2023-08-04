using System;
//Sample
// diff --git a/src/Website.BackEnd/SignalR/Hubs/RfsHubImpl.cs b/src/Website.BackEnd/SignalR/Hubs/RfsHubImpl.cs
// @@ -35,8 +35,6 @@ using Microsoft.AspNetCore.SignalR;
// diff --git a/src/Website.BackEnd/SignalR/Impl/CancellableItemCache.cs b/src/Website.BackEnd/SignalR/Impl/CancellableItemCache.cs
// @@ -28,7 +28,6 @@ using System;
// @@ -60,9 +59,12 @@ public class CancellableItemCache<TKey, TValue>
// @@ -187,4 +189,4 @@ public class CancellableItemCache<TKey, TValue>

var lastDiff = "";
var file = "";
var ln = 0;
while( Console.ReadLine() is {} line) 
{
    ln++;
    try
    {
        if (line.StartsWith("diff "))
        {
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

            var summary = line[(split+2)..];
            Console.WriteLine($"{file}|{cln}| {psp[1]} => {summary}");
        }
    }
    catch(Exception ex)
    {
        Console.Error.WriteLine($"Line: {ln} | {line}");
        Console.Error.WriteLine(ex.ToString());
    }
}
