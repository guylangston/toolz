// args[0] = directory/file
// args[1, optional = regex match (ignored if args[0] is file)

using System.Text.RegularExpressions;

var watchPath = args.First();
if (!Path.Exists(watchPath)) throw new Exception($"Path not found: '{watchPath}'");

Regex? match = null;
if (args.Length >= 2)
{
    match = new Regex(args[1]);
}


var path = GetPathInfo(watchPath);
var dir = path is DirectoryInfo d ? d : ((FileInfo)path).Directory!;

using var watcher = new FileSystemWatcher(dir.FullName);

watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
watcher.Changed += (o,e) => {
    if (IsMatch(e))
    {
        Console.WriteLine(e.FullPath);
    }
};


watcher.EnableRaisingEvents = true;

Console.ReadLine();


static FileSystemInfo GetPathInfo(string path)
{
    if (Directory.Exists(path)) return new DirectoryInfo(path);
    if (File.Exists(path)) return new FileInfo(path);
    throw new Exception($"Path not found: {path}");
}
bool IsMatch(FileSystemEventArgs e)
{
    if (match != null)
    {
        return match.IsMatch(e.FullPath);
    }
    if (path is FileInfo fileInfo)
    {
        return fileInfo.FullName == e.FullPath;
    }
    return false;
}
