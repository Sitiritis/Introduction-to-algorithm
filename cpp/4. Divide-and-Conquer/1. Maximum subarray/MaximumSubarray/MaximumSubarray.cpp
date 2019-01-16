// MaximumSubarray.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <tuple>
#include <vector>
#include <safeint.h>
#include "../../../common/assertions.h"
#include "../../../common/common.h"

// Calculates the maximum subarray that has the mid_idx & mid_idx + 1 elements
// of the array in_arr. 0 <= low_idx <= mid_idx < mid_idx + 1 <= high_idx < in_arr.size() <= max_val(size_t),
// which infers 2 <= in_arr.size() <= max_val(size_t)
// Returns a tuple:
// 1 element the lower index of the max subarray
// 2 element the high index of the max subarray
// 3 element sum of the max subarray
template <typename T>
inline std::tuple<size_t, size_t, T> max_crossing_subarray(
  std::vector<T>& in_arr,
  size_t low_idx,
  size_t high_idx,
  size_t mid_idx)
{
  #pragma region Constraints
  // check if the type of the elements in the input collection is of integer numeric types
  INTEGER_NUMERIC_TYPE_CHECK;

  #pragma region ArgsInputValues
  // invalid array size validation
  if (in_arr.size() < 2)
  {
    throw std::invalid_argument("The array must have at least 2 elements");
  }

  // check bounds of the input indexes
  if (
         (low_idx < 0)
      || (mid_idx < low_idx)
      || (mid_idx > high_idx)
      || (high_idx <= low_idx)
      || (high_idx >= in_arr.size())
      //|| (high_idx == std::numeric_limits<size_t>::max()
      // impossible, as in this case the size of the array would be
      // max_val(size_t) + 1, which is > than max_val(size_t)
     )
  {
    throw std::invalid_argument("The bounds have to statisfy the condition: 0 <= low_idx <= mid_idx < mid_idx + 1 <= high_idx < in_arr.size() <= max_val(size_t)");
  }
  #pragma endregion //ArgsInputValues
  #pragma endregion // Constraints

  #pragma region Logic
  // left from the midpoint
  T side_total_sum = in_arr[mid_idx], left_sum = side_total_sum;
  size_t low_max_idx = mid_idx;

  for (long i = mid_idx - 1; i >= static_cast<long>(low_idx); --i)
  {
    // msl::utilities::SafeAdd safely adds 2 integers, returning false if an
    // overflow has occured
    if (!msl::utilities::SafeAdd(side_total_sum, in_arr[i], side_total_sum))
    {
      throw std::overflow_error("An integer overflow has occured");
    }

    if (side_total_sum > left_sum)
    {
      left_sum = side_total_sum;
      low_max_idx = i;
    }
  }

  // right from the midpoint
  side_total_sum = in_arr[mid_idx + 1];
  T right_sum = side_total_sum;
  size_t high_max_idx = mid_idx + 1;

  for (size_t i = mid_idx + 2; i <= high_idx; ++i)
  {
    if (!msl::utilities::SafeAdd(side_total_sum, in_arr[i], side_total_sum))
    {
      throw std::overflow_error("An integer overflow has occured");
    }

    if (side_total_sum > right_sum)
    {
      right_sum = side_total_sum;
      high_max_idx = i;
    }
  }

  return { low_max_idx, high_max_idx, right_sum + left_sum }; // same as: std::tuple<size_t, size_t, T>({...});
  #pragma endregion // Logic
}

template <typename T>
std::tuple<size_t, size_t, T> max_subarray_recursion(
  std::vector<T>& in_arr,
  size_t low_idx,
  size_t high_idx)
{
  #pragma region Constraints
  // check if the type of the elements in the input collection is of integer numeric types
  INTEGER_NUMERIC_TYPE_CHECK;

  #pragma region ArgsInputValues
  if (in_arr.empty())
  {
    throw std::invalid_argument("The array has to be non-empty");
  }

  if (
          (low_idx < 0)
       || (high_idx < low_idx)
       || (high_idx >= in_arr.size())
     )
  {
    throw std::invalid_argument("The bounds have to statisfy the condition: 0 <= low_idx <= high_idx < in_arr.size() <= max_val(size_t)");
  }
  #pragma endregion // ArgsInputValues
  #pragma endregion // Constraints

  #pragma region Logic
  if (low_idx == high_idx)
  {
    return {low_idx, high_idx, in_arr[low_idx]};
  }
  else
  {
    size_t mid = (high_idx + low_idx) / 2;
    auto left_max     = max_subarray_recursion(in_arr, low_idx, mid),
         right_max    = max_subarray_recursion(in_arr, mid + 1, high_idx),
         crossing_max = max_crossing_subarray( in_arr, low_idx, high_idx, mid);

    if (
            (std::get<2>(left_max) >= std::get<2>(right_max))
         && (std::get<2>(left_max) >= std::get<2>(crossing_max))
       )
    {
      return left_max;
    }
    else if (
                 (std::get<2>(right_max) >= std::get<2>(crossing_max))
              && (std::get<2>(right_max) >= std::get<2>(left_max))
            )
    {
      return right_max;
    }
    else
    {
      return crossing_max;
    }
  }
  #pragma endregion // Logic
}

template <typename T>
std::tuple<size_t, size_t, T> max_subarray(std::vector<T>& in_arr)
{
  #pragma region Constraints
  // check if the type of the elements in the input collection is of integer numeric types
  INTEGER_NUMERIC_TYPE_CHECK;
  
  #pragma region ArgsInputValues
  if (in_arr.empty())
  {
    throw std::invalid_argument("The array has to be non-empty");
  }
  #pragma endregion // ArgsInputValues
  #pragma endregion // Constraints

  return max_subarray_recursion(in_arr, 0, in_arr.size() - 1);
}

void main()
{
  std::vector<int> arr{ 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7};
  print_array(arr);

  auto res = max_subarray(arr);

  std::cout << "Low index " << std::get<0>(res) << "\n";
  std::cout << "High index " << std::get<1>(res) << "\n";
  std::cout << "Sum " << std::get<2>(res) << "\n";
}
