// Merge sort.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <vector>

template<class T>
void merge(std::vector<T>& inArr, size_t firstIdx, size_t midIdx, size_t lastIdx)
{
  auto firstIt = inArr.cbegin() + firstIdx;
  auto midIt = inArr.cbegin() + midIdx + 1;
  auto lastIt = inArr.cbegin() + lastIdx + 1;
  std::vector<T>* left = new std::vector<T>(firstIt, midIt);
  std::vector<T>* right = new std::vector<T>(midIt, lastIt);

  size_t k, i, j;
  for (k = firstIdx, i = 0, j = 0
    // compare the elements from left and right
    // and put the lowest at k-th position
    // until one of the vectors is exhausted
    ; (k <= lastIdx) &&
      (i < left->size()) &&
      (j < right->size())
    ; ++k)
  {
    if (left->at(i) <= right->at(j))
    {
      inArr[k] = left->at(i);
      ++i;
    }
    else
    {
      inArr[k] = right->at(j);
      ++j;
    }
  }

  // spill the rest from the left to the original vector
  for (; i < left->size(); ++i, ++k)
  {
    inArr[k] = left->at(i);
  }

  // spill the rest from the right to the original vector
  for (; j < right->size(); ++j, ++k)
  {
    inArr[k] = right->at(j);
  }

  delete left;
  delete right;
}

template<class T>
void mergeSort(std::vector<T>& inArr, size_t firstIdx, size_t lastIdx)
{
  if (firstIdx < lastIdx)
  {
    size_t midIdx = (firstIdx + lastIdx) / 2; // integer division (for example, if lastIdx == 7, midIdx == 3)
    mergeSort(inArr, firstIdx, midIdx);
    mergeSort(inArr, midIdx + 1, lastIdx);
    merge(inArr, firstIdx, midIdx, lastIdx);
  }
}

template<class T>
void sortMerge(std::vector<T>& inArr)
{
  mergeSort(inArr, 0, (inArr.size() == 0) ? 0 : inArr.size() - 1);
}

template<class T>
void printArray(std::vector<T>& inArr)
{
  for (T el : inArr)
  {
    std::cout << el << "\n";
  }

  std::cout << "\n";
}

int main()
{
  std::vector<int>* arr = new std::vector<int>({3, 41, 52, 26, 38, 57, 9, 49});

  printArray(*arr);
  sortMerge(*arr);
  printArray(*arr);

  delete arr;
  std::cin;
}
