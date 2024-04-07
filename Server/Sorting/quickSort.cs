using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.Sorting
{
    public class QuickSort<T> : ISorter<T> where T : IComparable<T>
    {
        public  void SortAscending(T[] ArrayToSort) {
            ArrayToSort = QuickSortAscending(ArrayToSort, 0, ArrayToSort.Length - 1);
        }

        private T[] QuickSortAscending(T[] ArrayToSort, int leftIndex, int rightIndex)
        {
            int index_1 = leftIndex;
            int index_2 = rightIndex;
            T pivot = ArrayToSort[leftIndex];
            while (index_1 <= index_2)
            {
                while (ArrayToSort[index_1].CompareTo(pivot) < 0)
                {
                    index_1++;
                }

                while (ArrayToSort[index_2].CompareTo(pivot) > 0)
                {
                    index_2--;
                }

                if (index_1 <= index_2)
                {
                    T TemporaryVariableForSwitchingValues = ArrayToSort[index_1];
                    ArrayToSort[index_1] = ArrayToSort[index_2];
                    ArrayToSort[index_2] = TemporaryVariableForSwitchingValues;
                    index_1++;
                    index_2--;
                }
            }

            if (leftIndex < index_2)
                QuickSortAscending(ArrayToSort, leftIndex, index_2);
            if (index_1 < rightIndex)
                QuickSortAscending(ArrayToSort, index_1, rightIndex);
            return ArrayToSort;
        }

        public  void SortAscending(List<T> ListToSort) {
            ListToSort = QuickSortAscending(ListToSort, 0, ListToSort.Count - 1);
        }

        private  List<T> QuickSortAscending(List<T> ListToSort, int leftIndex, int rightIndex)
        {
            int index_1 = leftIndex;
            int index_2 = rightIndex;
            T pivot = ListToSort[leftIndex];
            while (index_1 <= index_2)
            {
                while (ListToSort[index_1].CompareTo(pivot) < 0)
                {
                    index_1++;
                }

                while (ListToSort[index_2].CompareTo(pivot) > 0)
                {
                    index_2--;
                }

                if (index_1 <= index_2)
                {
                    T TemporaryVariableForSwitchingValues = ListToSort[index_1];
                    ListToSort[index_1] = ListToSort[index_2];
                    ListToSort[index_2] = TemporaryVariableForSwitchingValues;
                    index_1++;
                    index_2--;
                }
            }

            if (leftIndex < index_2)
                QuickSortAscending(ListToSort, leftIndex, index_2);
            if (index_1 < rightIndex)
                QuickSortAscending(ListToSort, index_1, rightIndex);
            return ListToSort;
        }

        public  void SortAscending(T[] ArrayToSort, Func<T, T, int> comparisonFunction)
        {
            ArrayToSort = QuickSortAscending(ArrayToSort, 0, ArrayToSort.Length - 1, comparisonFunction);
        }

        private  T[] QuickSortAscending(T[] ArrayToSort, int leftIndex, int rightIndex, Func<T, T, int> comparisonFunction)
        {
            int index_1 = leftIndex;
            int index_2 = rightIndex;
            T pivot = ArrayToSort[leftIndex];
            while (index_1 <= index_2)
            {
                while (comparisonFunction(ArrayToSort[index_1], pivot) < 0)
                {
                    index_1++;
                }

                while (comparisonFunction(ArrayToSort[index_2], pivot) > 0)
                {
                    index_2--;
                }

                if (index_1 <= index_2)
                {
                    T TemporaryVariableForSwitchingValues = ArrayToSort[index_1];
                    ArrayToSort[index_1] = ArrayToSort[index_2];
                    ArrayToSort[index_2] = TemporaryVariableForSwitchingValues;
                    index_1++;
                    index_2--;
                }
            }

            if (leftIndex < index_2)
                QuickSortAscending(ArrayToSort, leftIndex, index_2, comparisonFunction);
            if (index_1 < rightIndex)
                QuickSortAscending(ArrayToSort, index_1, rightIndex, comparisonFunction);
            return ArrayToSort;
        }


        public  void SortAscending(List<T> ListToSort, Func<T, T, int> comparisonFunction)
        {
            ListToSort = QuickSortAscending(ListToSort, 0, ListToSort.Count - 1, comparisonFunction);
        }

        private  List<T> QuickSortAscending(List<T> ListToSort, int leftIndex, int rightIndex, Func<T, T, int> comparisonFunction)
        {
            int index_1 = leftIndex;
            int index_2 = rightIndex;
            T pivot = ListToSort[leftIndex];
            while (index_1 <= index_2)
            {
                while (comparisonFunction(ListToSort[index_1], pivot) < 0)
                {
                    index_1++;
                }

                while (comparisonFunction(ListToSort[index_2], pivot) > 0)
                {
                    index_2--;
                }

                if (index_1 <= index_2)
                {
                    T TemporaryVariableForSwitchingValues = ListToSort[index_1];
                    ListToSort[index_1] = ListToSort[index_2];
                    ListToSort[index_2] = TemporaryVariableForSwitchingValues;
                    index_1++;
                    index_2--;
                }
            }

            if (leftIndex < index_2)
                QuickSortAscending(ListToSort, leftIndex, index_2,comparisonFunction);
            if (index_1 < rightIndex)
                QuickSortAscending(ListToSort, index_1, rightIndex,comparisonFunction);
            return ListToSort;
        }
    }
}
