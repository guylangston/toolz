var lines = new List<string>();
while(Console.ReadLine() is {} line) lines.Add(line);

foreach(var line in lines
        .OrderByDescending(x=>x.StartsWith("using System"))
        .ThenByDescending(x=>x.StartsWith("using Microsoft"))
        .ThenBy(x=>x))
{
    Console.WriteLine(line);
}
