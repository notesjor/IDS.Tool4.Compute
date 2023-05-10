using System.Text;

namespace SplitTsv
{
  internal class Program
  {
    private static FileStream _fsO = null;
    private static StreamWriter _writer;

    static void Main(string[] args)
    {
      var input = args[0];
      var max = int.Parse(args[1]);
      var output = args[2];

      var name = Path.GetFileNameWithoutExtension(input);
      var cluster = 1;
      var count = 0;

      using (var fsI = new FileStream(input, FileMode.Open, FileAccess.Read))
      using (var reader = new StreamReader(fsI, Encoding.UTF8))
      {
        var header = reader.ReadLine();
        NewCluster(output, name, header, ref cluster, ref count);

        while (!reader.EndOfStream)
        {
          if (count > max)
            NewCluster(output, name, header, ref cluster, ref count);

          _writer.WriteLine(reader.ReadLine());

          count++;
        }
      }
    }

    private static void NewCluster(string output, string name, string header, ref int cluster, ref int count)
    {
      // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
      if (_fsO != null)
      {
        _writer.Close();
        _fsO.Close();
      }

      _fsO = new FileStream(Path.Combine(output, $"{name}_{cluster:D3}.tsv"), FileMode.Create, FileAccess.Write);
      _writer = new StreamWriter(_fsO, Encoding.UTF8);

      _writer.WriteLine(header);

      cluster++;
      count = 0;
    }
  }
}