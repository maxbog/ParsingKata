using System;
using ParsingKata.Utils;

namespace ParsingKata
{
  class Program
  {
    static void Main(string[] args)
    {
      Optional<int> opt = Optional<int>.Of(3);
      Optional<string> mapped = opt.FlatMap(value =>
      {
        return Optional<string>.Empty;
      });
      Console.WriteLine(mapped.Get());
      Console.ReadKey();
    }
  }
}
