using System;
using System.Collections.Generic;

namespace IntroductionToAlgorithms
{
  public static class MaximumSubarray
  {
    private static (int lowMaxIdx, int highMaxIdx, long totalSum)
      maximumCrossingSubarray
      (IList<long> inArr, int lowIdx, int midIdx, int highIdx)
    {
      #region Preconditions
      if (inArr.Count < 2)
      {
        throw new ArgumentException("The array must have more than 1 element");
      }

      if (
        (lowIdx < 0) ||
        (midIdx < lowIdx) ||
        (midIdx >= highIdx) ||
        (highIdx <= lowIdx) ||
        (highIdx >= inArr.Count)
      )
      {
        throw new ArgumentException("Indexes must satisfy the inequality: 0 <= lowIdx <= midIdx < highIdx < inArr.Count");
      }
      #endregion // Preconditions

      #region Logic
      int lowMaxIdx = midIdx;
      long leftSum = inArr[lowMaxIdx], sideSum = leftSum;

      for (int i = midIdx - 1; i >= lowIdx; --i)
      {
        sideSum += inArr[i];

        if (sideSum > leftSum)
        {
          leftSum = sideSum;
          lowMaxIdx = i;
        }
      }

      int highMaxIdx = midIdx + 1;
      long rightSum = inArr[midIdx + 1];
      sideSum = rightSum;

      for (int i = midIdx + 2; i <= highIdx; ++i)
      {
        sideSum += inArr[i];

        if (sideSum > rightSum)
        {
          rightSum = sideSum;
          highMaxIdx = i;
        }
      }

      return (lowMaxIdx, highMaxIdx, leftSum + rightSum);
      #endregion // Logic
    }

    private static (int lowMaxIdx, int highMaxIdx, long totalSum)
      maximumSubarrayRecur
      (IList<long> inArr, int lowIdx, int highIdx)
    {
      #region Preconditions
      if (inArr.Count == 0)
      {
        throw new ArgumentException("The array cannot be empty");
      }

      if (
        (lowIdx < 0) ||
        (highIdx < lowIdx) ||
        (highIdx >= inArr.Count)
      )
      {
        throw new ArgumentException("Indexes must satisfy the inequality: 0 <= lowIdx <= highIdx < inArr.Count");
      }
      #endregion // Pretconditions

      #region Logic
      if (lowIdx == highIdx)
      {
        return (lowIdx, highIdx, inArr[lowIdx]);
      }
      else
      {
        int midIdx = (lowIdx + highIdx) / 2;

        var leftMaxSubarray = maximumSubarrayRecur(inArr, lowIdx, midIdx);
        var rightMaxSubarray = maximumSubarrayRecur(inArr, midIdx + 1, highIdx);
        var crossingMaxSubarray = maximumCrossingSubarray(inArr, lowIdx, midIdx, highIdx);

        if (
          (leftMaxSubarray.totalSum >= rightMaxSubarray.totalSum) &&
          (leftMaxSubarray.totalSum >= crossingMaxSubarray.totalSum)
        )
        {
          return leftMaxSubarray;
        }
        else if (
          (rightMaxSubarray.totalSum >= leftMaxSubarray.totalSum) &&
          (rightMaxSubarray.totalSum >= crossingMaxSubarray.totalSum)
        )
        {
          return rightMaxSubarray;
        }
        else
        {
          return crossingMaxSubarray;
        }
        #endregion // Logic
      }
    }

    public static (int lowMaxIdx, int highMaxIdx, long totalSum)
      maximumSubarrayDivideNConquer
      (IList<long> inArr)
    {
      #region Preconditions
      if (inArr.Count == 0)
      {
        throw new ArgumentException("The array cannot be empty");
      }
      #endregion // Pretconditions

      return maximumSubarrayRecur(inArr, 0, inArr.Count - 1);
    }

    public static (int lowMaxIdx, int highMaxIdx, long totalSum)
      maximumSubarrayBruteForce
      (IList<long> inArr)
    {
      #region Preconditions
      if (inArr.Count == 0)
      {
        throw new ArgumentException("The array cannot be empty");
      }
      #endregion // Pretconditions

      #region Logic
      long maxSum = inArr[0];
      int maxLowIdx = 0, maxHighIdx = 0;

      for (int i = 0; i < inArr.Count; ++i)
      {
        long runningSum = 0;

        for (int j = i; j < inArr.Count; ++j)
        {
          runningSum += inArr[j];

          if (runningSum > maxSum)
          {
            maxLowIdx = i;
            maxHighIdx = j;
            maxSum = runningSum;
          }
        }
      }

      return (maxLowIdx, maxHighIdx, maxSum);
      #endregion // Logic
    }

    private static (int lowMaxIdx, int highMaxIdx, long totalSum)
      maximumSubarrayHybridRecur
      (IList<long> inArr, int lowIdx, int highIdx)
    {
      #region Preconditions
      if (inArr.Count == 0)
      {
        throw new ArgumentException("The array cannot be empty");
      }

      if (
        (lowIdx < 0) ||
        (highIdx < lowIdx) ||
        (highIdx >= inArr.Count)
      )
      {
        throw new ArgumentException("Indexes must satisfy the inequality: 0 <= lowIdx <= highIdx < inArr.Count");
      }
      #endregion // Pretconditions

      #region Logic
      #region Brute-Force
      if ((highIdx - lowIdx) < 15)
      {
        long maxSum = inArr[lowIdx];
        int maxLowIdx = lowIdx, maxHighIdx = lowIdx;

        for (int i = lowIdx; i <= highIdx; ++i)
        {
          long runningSum = 0;

          for (int j = i; j <= highIdx; ++j)
          {
            runningSum += inArr[j];

            if (runningSum > maxSum)
            {
              maxLowIdx = i;
              maxHighIdx = j;
              maxSum = runningSum;
            }
          }
        }

        return (maxLowIdx, maxHighIdx, maxSum);
      }
      #endregion // Brute-Force
      #region Divide-and-Conquer
      else
      {
        int midIdx = (lowIdx + highIdx) / 2;

        var leftMaxSubarray = maximumSubarrayRecur(inArr, lowIdx, midIdx);
        var rightMaxSubarray = maximumSubarrayRecur(inArr, midIdx + 1, highIdx);
        var crossingMaxSubarray = maximumCrossingSubarray(inArr, lowIdx, midIdx, highIdx);

        if (
          (leftMaxSubarray.totalSum >= rightMaxSubarray.totalSum) &&
          (leftMaxSubarray.totalSum >= crossingMaxSubarray.totalSum)
        )
        {
          return leftMaxSubarray;
        }
        else if (
          (rightMaxSubarray.totalSum >= leftMaxSubarray.totalSum) &&
          (rightMaxSubarray.totalSum >= crossingMaxSubarray.totalSum)
        )
        {
          return rightMaxSubarray;
        }
        else
        {
          return crossingMaxSubarray;
        }
      }
      #endregion // Divide-and-Conquer
      #endregion // Logic
    }

    public static (int lowMaxIdx, int highMaxIdx, long totalSum)
      maximumSubarrayHybrid
      (IList<long> inArr)
    {
      #region Preconditions
      if (inArr.Count == 0)
      {
        throw new ArgumentException("The array cannot be empty");
      }
      #endregion // Pretconditions

      return maximumSubarrayHybridRecur(inArr, 0, inArr.Count - 1);
    }
  }
}
