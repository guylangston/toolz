var file = args.First();
var full = new FileInfo(file);
var dir = full.Directory.FullName;
using var watcher = new FileSystemWatcher(dir);

watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
watcher.Changed += (o,e) => {
    if (full.FullName == e.FullPath) {
        Console.WriteLine(e.FullPath);
    }
};
watcher.EnableRaisingEvents = true;

Console.ReadLine();
