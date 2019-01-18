using System;
using System.Collections.Generic;
using System.Collections;
using BenchmarkDotNet.Attributes;
using IntroductionToAlgorithms;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace BenchmarkMaxSubarray
{
  
  public class ArraysArgumentSizeColumn : IColumn
  {
    public string Id => nameof(ArraysArgumentSizeColumn);

    public string ColumnName => "Array size";

    public string Legend => "Size of an input array";

    public ColumnCategory Category => ColumnCategory.Custom;

    public int PriorityInCategory => 0;

    public bool AlwaysShow => true;

    public bool IsAvailable(Summary summary) => true;

    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;

    public bool IsNumeric => false;

    public UnitType UnitType => UnitType.Size;

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase) =>
      GetValue(summary, benchmarkCase, new SummaryStyle());

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, ISummaryStyle style)
    {
      string result = "No non-null array arguments";

      if (benchmarkCase.HasArguments || benchmarkCase.HasParameters)
      {
        result = "";

        for (int i = 0; i < benchmarkCase.Parameters.Count; ++i)
        {
          bool addSpace = false;

          if (benchmarkCase.Parameters[i].Value is IList)
          {
            result += ((addSpace) ? " " : "")
              + (style.SizeUnit.Name == "Print name" ? benchmarkCase.Parameters[i].Name : "")
              + ((IList)(benchmarkCase.Parameters[i].Value)).Count;
            addSpace = true;
          }
        }
      }

      return result;
    }
  }

  [Config(typeof(Config))]
  public class BenchmarkMaxSubarray
  {
    private class Config : ManualConfig
    {
      public Config()
      {
        Add(new ArraysArgumentSizeColumn());
        Add(new Job("Maximum subarray benchmark") {
          Run = { LaunchCount = 1, WarmupCount = 5, IterationCount = 10 }
        });
      }
    }

    public IEnumerable<object> generateArrays()
    {
      Random rnd = new Random((int)DateTime.Now.Ticks);

      for (int i = 1; i < 20; ++i)
      {
        List<long> genArr = new List<long>();

        for (int j = 0; j < i; ++j)
        {
          genArr.Add(rnd.Next(-100, 101));
        }

        yield return genArr;
      }
    }

    //[Benchmark]
    //[ArgumentsSource(nameof(generateArrays))]
    public (int lowMaxIdx, int highMaxIdx, long totalSum)
      maximumSubarrayDivideNConquer
     (IList<long> inArr)
    {
      //List<long> inArr = new List<long> { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };

      return MaximumSubarray.maximumSubarrayDivideNConquer(inArr);
    }

    //[Benchmark]
    //[ArgumentsSource(nameof(generateArrays))]
    public (int lowMaxIdx, int highMaxIdx, long totalSum)
     maximumSubarrayBruteForce
     (IList<long> inArr)
    {
      //List<long> inArr = new List<long> { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };

      return MaximumSubarray.maximumSubarrayBruteForce(inArr);
    }

    [Benchmark]
    [ArgumentsSource(nameof(generateArrays))]
    public (int lowMaxIdx, int highMaxIdx, long totalSum)
     maximumSubarrayHybrid
     (IList<long> inArr)
    {
      //List<long> inArr = new List<long> { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };

      return MaximumSubarray.maximumSubarrayHybrid(inArr);
    }

    [Benchmark]
    [ArgumentsSource(nameof(generateArrays))]
    public (int lowMaxIdx, int highMaxIdx, long totalSum)
     maximumSubarrayDynamic
     (IList<long> inArr)
    {
      //List<long> inArr = new List<long> { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };

      return MaximumSubarray.maximumSubarrayDynamic(inArr);
    }
  }
}
