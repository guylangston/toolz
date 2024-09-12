using System;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.IO;

internal class Program
{
    // $ dotnet run -- --md https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/
    // [Performance Improvements in .NET 8 - .NET Blog](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/)
    private static async Task<int> Main(string[] args)
    {
        var url = args.FirstOrDefault(x=>!x.StartsWith("-")) ?? Console.ReadLine();
        var markdown = args.Any(x=>x == "--md");

        using HttpClient client = new HttpClient();
        var stream = await client.GetStreamAsync(url);
        using var reader = new StreamReader(stream);
        for (var cc = 0; cc < 100; cc++)
        {
            var ln = await reader.ReadLineAsync();
            if (ln == null) return -1;

            var idxStart = ln.IndexOf("<title>", StringComparison.InvariantCultureIgnoreCase);
            if (idxStart < 0) continue;
            var idxEnd = ln.IndexOf("</title>", idxStart, StringComparison.InvariantCultureIgnoreCase);
            if (idxEnd < 0) continue;

            var title = ln[(idxStart + "<title>".Length)..idxEnd];
            var text = HttpUtility.HtmlDecode(title);

            if (markdown)
            {
                Console.WriteLine($"[{text}]({url})");
            }
            else
            {
                Console.WriteLine(text);
            }
            return 0;
        }
        return -1;
    }
}
