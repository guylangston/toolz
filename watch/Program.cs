// args[0] = directory/file
// args[1, optional = regex match (ignored if args[0] is file)

using System.Text.RegularExpressions;
var opts = args.Where(x=>x.StartsWith("-")).ToArray();
var parms = args.Where(x=>!x.StartsWith("-")).ToArray();;
var argsPath = parms.FirstOrDefault() ?? "./";
var argsMatch = parms.Length > 1 ? args[1] : null;
var argsRel = opts.Any(x=>x == "-r");
var argsDebug = opts.Any(x=>x == "-d");

if (!Path.Exists(argsPath)) throw new Exception($"Path not found: '{argsPath}'");

Regex? match = null;
if (argsMatch is {})
{
    Log($"Using regex: {args[1]}");
    match = new Regex(args[1]);
}

var path = GetPathInfo(argsPath);
var dir = path is DirectoryInfo d ? d : ((FileInfo)path).Directory!;

Log($"Watching: {dir.FullName}");

using var watcher = new FileSystemWatcher(dir.FullName);
watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
watcher.IncludeSubdirectories = true;
watcher.Changed += (o,e) => {
    if (!File.Exists(e.FullPath)) return; // only accept file changes
    if (IsMatch(e))
    {
        if (argsRel) {
            Console.WriteLine("." + Path.DirectorySeparatorChar + Path.GetRelativePath(dir.FullName, e.FullPath));
        }
        else {
            Console.WriteLine(e.FullPath);
        }
    }
    else
    {
        Log($"Skipped: {e.FullPath} : {e.Name} ({e.ChangeType})");
    }
};

watcher.EnableRaisingEvents = true;
Console.ReadLine();
return 0;   // exit...

void Log(string txt)
{
    if (argsDebug) Console.WriteLine($"# {txt}");
       else System.Diagnostics.Debug.WriteLine($"# {txt}");
}

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
    if (File.Exists(e.FullPath)) return true;
    return false;
}

