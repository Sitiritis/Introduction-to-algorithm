using System;
using BenchmarkDotNet.Running;

namespace BenchmarkMaxSubarray
{
  class Program
  {
    static void Main(string[] args)
    {
      var benchmark = BenchmarkRunner.Run<BenchmarkMaxSubarray>();

      Console.ReadKey();
    }
  }
}
