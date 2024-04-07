using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.Sorting
{
    public  class GnomeSort<T> : ISorter<T> where T : IComparable<T>
    {
        public  void SortAscending(T[] ArrayToSort) {
            int index = 1;
            int numberOfElements = ArrayToSort.Length;
            while (index < numberOfElements)
            {
                if (index == 0)
                    index++;
                if (ArrayToSort[index].CompareTo(ArrayToSort[index - 1]) >= 0)
                    index++;
                else
                {
                    T TemporaryVariableForSwitchingValues;
                    TemporaryVariableForSwitchingValues = ArrayToSort[index];
                    ArrayToSort[index] = ArrayToSort[index - 1];
                    ArrayToSort[index - 1] = TemporaryVariableForSwitchingValues;
                    index--;
                }
            }
        }
        public  void SortAscending(List<T> ListToSort)
        {
            int index = 1;
            int numberOfElements = ListToSort.Count;
            while (index < numberOfElements)
            {
                if (index == 0)
                    index++;
                if (ListToSort[index].CompareTo(ListToSort[index - 1]) >= 0)
                    index++;
                else
                {
                    T TemporaryVariableForSwitchingValues;
                    TemporaryVariableForSwitchingValues = ListToSort[index];
                    ListToSort[index] = ListToSort[index - 1];
                    ListToSort[index - 1] = TemporaryVariableForSwitchingValues;
                    index--;
                }
            }
        }

        public  void SortAscending(T[] ArrayToSort, Func<T, T, int> comparisonFunction) {
            int index = 1;
            int numberOfElements = ArrayToSort.Length;
                while (index<numberOfElements)
                {
                    if (index == 0)
                        index++;
                    if (comparisonFunction(ArrayToSort[index],ArrayToSort[index - 1]) >= 0)
                        index++;
                    else
                    {
                        T TemporaryVariableForSwitchingValues;
                        TemporaryVariableForSwitchingValues = ArrayToSort[index];
                        ArrayToSort[index] = ArrayToSort[index - 1];
                        ArrayToSort[index - 1] = TemporaryVariableForSwitchingValues;
                        index--;
                    }
                }
        }
        public  void SortAscending(List<T> ListToSort, Func<T, T, int> comparisonFunction)
        {
            int index = 1;
            int numberOfElements = ListToSort.Count;
            while (index < numberOfElements)
            {
                if (index == 0)
                    index++;
                if (comparisonFunction(ListToSort[index], ListToSort[index - 1]) >= 0)
                    index++;
                else
                {
                    T TemporaryVariableForSwitchingValues;
                    TemporaryVariableForSwitchingValues = ListToSort[index];
                    ListToSort[index] = ListToSort[index - 1];
                    ListToSort[index - 1] = TemporaryVariableForSwitchingValues;
                    index--;
                }
            }
        }
    }
}
