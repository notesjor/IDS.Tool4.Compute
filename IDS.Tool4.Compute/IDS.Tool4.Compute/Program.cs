using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO.Compression;

namespace IDS.Tool4.Compute
{
  internal class Program
  {
    private static object _lock_files = new object();

    static void Main(string[] args)
    {
      if (args.Length < 2)
        Help();

      if (args[1] == "del")
        Delete(args[2], Environment.ProcessorCount);

      if (args[1] == "scan")
        Scan(args[2], Environment.ProcessorCount);
    }

    private static void Scan(string path, int parallel, out long countFiles, out long sumSize)
    {
      Parallel.ForEach(Directory.GetDirectories(path), new ParallelOptions { MaxDegreeOfParallelism = parallel }, dir =>
      {
        var cores = parallel / 2;
        Scan(dir, cores < 1 ? 1 : cores);
      });

      var files = Directory.GetFiles(path);

      lock (_lock_files)
        Parallel.ForEach(files, file =>
        {
          try
          {
            
          }
          catch
          {
            // ignore
          }
        });

      try
      {
        
      }
      catch
      {
        // ignore
      }
    }

    private static void Delete(string path, int parallel)
    {
      Parallel.ForEach(Directory.GetDirectories(path), new ParallelOptions { MaxDegreeOfParallelism = parallel }, dir =>
      {
        var cores = parallel / 2;
        Delete(dir, cores < 1 ? 1 : cores);
      });

      var files = Directory.GetFiles(path);

      lock (_lock_files)
        Parallel.ForEach(files, file =>
        {
          try
          {
            File.Delete(file);
          }
          catch
          {
            // ignore
          }
        });

      try
      {
        Directory.Delete(path, true);
      }
      catch
      {
        // ignore
      }
    }

    private static void Help()
    {
      Console.WriteLine("IDS Tool4 compute-Node");
      Console.WriteLine();
      Console.WriteLine("del [DIRECTORY] - löscht ein Verzeichnis sehr schnell");
      Console.WriteLine("scan [DIRECTORY] - analysiert die Unterverzeichnisse von [DIRECTORY] - Anzahl Dateien + Summe Dateigröße. Ordner mit .nobackup werden ignoriert.");
    }
  }
}