// Insertion sort.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <vector>

template<class T>
void insertionSortAsc(std::vector<T>& inArr)
{
  for (unsigned int i = 1; i < inArr.size(); ++i)
  {
    T buff = inArr[i];
    int j = i - 1;

    while (j >= 0 && inArr[j] > buff)
    {
      inArr[j + 1] = inArr[j];
      --j;
    }

    inArr[j + 1] = buff;
  }
}

template<class T>
void insertionSortDesc(std::vector<T>& inArr)
{
  for (unsigned int i = 1; i < inArr.size(); ++i)
  {
    T buff = inArr[i];
    int j = i - 1;

    while (j >= 0 && inArr[j] < buff)
    {
      inArr[j + 1] = inArr[j];
      --j;
    }

    inArr[j + 1] = buff;
  }
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
  std::vector<int>* arr = new std::vector<int>({5, 2, 4, 6, 1, 3});

  printArray(*arr);
  insertionSortAsc(*arr);
  printArray(*arr);
  insertionSortDesc(*arr);
  printArray(*arr);

  std::cin;
}
