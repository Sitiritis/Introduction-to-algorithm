using System;
using IntroductionToAlgorithms;
using System.Collections.Generic;

namespace MaximumSubarrayConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      List<long> arr = new List<long> { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };

      var maxSubarrayDnC = MaximumSubarray.maximumSubarrayDivideNConquer(arr);
      Console.WriteLine("Divide-n-Conquer\nLow index = {0}\nHigh index = {1}\nSum = {2}\n",
        maxSubarrayDnC.lowMaxIdx, maxSubarrayDnC.highMaxIdx, maxSubarrayDnC.totalSum
      );

      var maxSubarrayBF = MaximumSubarray.maximumSubarrayBruteForce(arr);
      Console.WriteLine("Brute-force\nLow index = {0}\nHigh index = {1}\nSum = {2}\n",
        maxSubarrayBF.lowMaxIdx, maxSubarrayBF.highMaxIdx, maxSubarrayBF.totalSum
      );

      var maxSubarrayHybrid = MaximumSubarray.maximumSubarrayHybrid(arr);
      Console.WriteLine("Hybrid\nLow index = {0}\nHigh index = {1}\nSum = {2}\n",
        maxSubarrayHybrid.lowMaxIdx, maxSubarrayHybrid.highMaxIdx, maxSubarrayHybrid.totalSum
      );

      Console.ReadKey();
    }
  }
}
