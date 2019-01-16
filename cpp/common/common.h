#include <vector>

template<class T>
void print_array(std::vector<T>& inArr)
{
  for (T el : inArr)
  {
    std::cout << el << "\n";
  }

  std::cout << "\n";
}
