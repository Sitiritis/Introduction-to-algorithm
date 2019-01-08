#include <type_traits>

#define INTEGER_NUMERIC_TYPE_CHECK \
  static_assert( \
    std::is_same<T, short>::value || \
    std::is_same<T, short int>::value || \
    std::is_same<T, signed short>::value || \
    std::is_same<T, signed short int>::value || \
    std::is_same<T, unsigned short>::value || \
    std::is_same<T, unsigned short int>::value || \
    std::is_same<T, int>::value || \
    std::is_same<T, signed>::value || \
    std::is_same<T, signed int>::value || \
    std::is_same<T, unsigned>::value || \
    std::is_same<T, unsigned int>::value || \
    std::is_same<T, long>::value || \
    std::is_same<T, long int>::value || \
    std::is_same<T, signed long>::value || \
    std::is_same<T, signed long int>::value || \
    std::is_same<T, unsigned long>::value || \
    std::is_same<T, unsigned long int>::value || \
    std::is_same<T, long long>::value || \
    std::is_same<T, long long int>::value || \
    std::is_same<T, signed long long>::value || \
    std::is_same<T, signed long long int>::value || \
    std::is_same<T, unsigned long long>::value || \
    std::is_same<T, unsigned long long int>::value, \
    "The type of the elements in the collection should be some of the integer numeric types. Please, refer to https://en.cppreference.com/w/cpp/language/types"\
  );
