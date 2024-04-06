using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Sort
{
    public  class MergeSort<T> : ISorter<T> where T : IComparable<T>
    {

        public  void SortAscending(T[] ArrayToSort) 
        {
            ArrayToSort = SortAndMergeAscending(ArrayToSort, 0, ArrayToSort.Length-1);
        }

        public  void MergeAscending(T[] ArrayToMerge, int left, int middle, int right)
        {
            int leftLength = middle - left + 1;
            int rightLength = right - middle;

            T[] leftTemporaryArray = new T[leftLength];
            T[] rightTemporaryArray = new T[rightLength];
            int index_1, index_2;

            for (index_1 = 0; index_1 < leftLength; index_1++)
                leftTemporaryArray[index_1] = ArrayToMerge[left + index_1];
            for (index_2 = 0; index_2 < rightLength; index_2++)
                rightTemporaryArray[index_2] = ArrayToMerge[middle + index_2 + 1];

            index_1 = 0;
            index_2 = 0;

            int indexForSorting = left;
            while (index_1 < leftLength && index_2 < rightLength)
            {
                if (leftTemporaryArray[index_1].CompareTo(rightTemporaryArray[index_2]) <= 0)
                {
                    ArrayToMerge[indexForSorting] = leftTemporaryArray[index_1];
                    index_1++;
                }
                else
                {
                    ArrayToMerge[indexForSorting] = rightTemporaryArray[index_2];
                    index_2++;
                }
                indexForSorting++;
            }

            while (index_1 < leftLength)
            {
                ArrayToMerge[indexForSorting] = leftTemporaryArray[index_1];
                index_1++;
                indexForSorting++;
            }

            while (index_2 < rightLength)
            {
                ArrayToMerge[indexForSorting] = rightTemporaryArray[index_2];
                index_2++;
                indexForSorting++;
            }
        }

        public  T[] SortAndMergeAscending(T[] ArrayToSort, int left, int right) 
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                SortAndMergeAscending(ArrayToSort, left, middle);
                SortAndMergeAscending(ArrayToSort, middle + 1, right);
                MergeAscending(ArrayToSort, left, middle, right);
            }

            return ArrayToSort;
        }
        public  void SortAscending(List<T> ListToSort)
        {
            ListToSort = SortAndMergeAscending(ListToSort, 0, ListToSort.Count - 1);
        }

        public static void MergeAscending(List<T> ListToMerge, int left, int middle, int right)
        {
            int leftLength = middle - left + 1;
            int rightLength = right - middle;

            T[] leftTemporaryArray = new T[leftLength];
            T[] rightTemporaryArray = new T[rightLength];
            int index_1, index_2;

            for (index_1 = 0; index_1 < leftLength; index_1++)
                leftTemporaryArray[index_1] = ListToMerge[left + index_1];
            for (index_2 = 0; index_2 < rightLength; index_2++)
                rightTemporaryArray[index_2] = ListToMerge[middle + index_2 + 1];

            index_1 = 0;
            index_2 = 0;

            int indexForSorting = left;
            while (index_1 < leftLength && index_2 < rightLength)
            {
                if (leftTemporaryArray[index_1].CompareTo(rightTemporaryArray[index_2]) <= 0)
                {
                    ListToMerge[indexForSorting] = leftTemporaryArray[index_1];
                    index_1++;
                }
                else
                {
                    ListToMerge[indexForSorting] = rightTemporaryArray[index_2];
                    index_2++;
                }
                indexForSorting++;
            }

            while (index_1 < leftLength)
            {
                ListToMerge[indexForSorting] = leftTemporaryArray[index_1];
                index_1++;
                indexForSorting++;
            }

            while (index_2 < rightLength)
            {
                ListToMerge[indexForSorting] = rightTemporaryArray[index_2];
                index_2++;
                indexForSorting++;
            }
        }

        public  List<T> SortAndMergeAscending(List<T> ListToSort, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                SortAndMergeAscending(ListToSort, left, middle);
                SortAndMergeAscending(ListToSort, middle + 1, right);
                MergeAscending(ListToSort, left, middle, right);
            }
            return ListToSort;
        }

        public  void SortAscending(T[] ArrayToSort, Func<T, T, int> comparisonFunction)
        {
            ArrayToSort = SortAndMergeAscending(ArrayToSort, 0, ArrayToSort.Length - 1, comparisonFunction);
        }

        public  void MergeAscending(T[] ArrayToMerge, int left, int middle, int right, Func<T, T, int> comparisonFunction)
        {
            int leftLength = middle - left + 1;
            int rightLength = right - middle;

            T[] leftTemporaryArray = new T[leftLength];
            T[] rightTemporaryArray = new T[rightLength];
            int index_1, index_2;

            for (index_1 = 0; index_1 < leftLength; index_1++)
                leftTemporaryArray[index_1] = ArrayToMerge[left + index_1];
            for (index_2 = 0; index_2 < rightLength; index_2++)
                rightTemporaryArray[index_2] = ArrayToMerge[middle + index_2 + 1];

            index_1 = 0;
            index_2 = 0;

            int indexForSorting = left;
            while (index_1 < leftLength && index_2 < rightLength)
            {
                if (comparisonFunction(leftTemporaryArray[index_1], rightTemporaryArray[index_2]) <= 0)
                {
                    ArrayToMerge[indexForSorting] = leftTemporaryArray[index_1];
                    index_1++;
                }
                else
                {
                    ArrayToMerge[indexForSorting] = rightTemporaryArray[index_2];
                    index_2++;
                }
                indexForSorting++;
            }

            while (index_1 < leftLength)
            {
                ArrayToMerge[indexForSorting] = leftTemporaryArray[index_1];
                index_1++;
                indexForSorting++;
            }

            while (index_2 < rightLength)
            {
                ArrayToMerge[indexForSorting] = rightTemporaryArray[index_2];
                index_2++;
                indexForSorting++;
            }
        }

        public  T[] SortAndMergeAscending(T[] ArrayToSort, int left, int right, Func<T, T, int> comparisonFunction)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                SortAndMergeAscending(ArrayToSort, left, middle, comparisonFunction);
                SortAndMergeAscending(ArrayToSort, middle + 1, right, comparisonFunction);
                MergeAscending(ArrayToSort, left, middle, right, comparisonFunction);
            }

            return ArrayToSort;
        }

        public  void SortAscending(List<T> ListToSort, Func<T, T, int> comparisonFunction)
        {
            ListToSort = SortAndMergeAscending(ListToSort, 0, ListToSort.Count - 1, comparisonFunction);
        }

        public void MergeAscending(List<T> ListToMerge, int left, int middle, int right, Func<T, T, int> comparisonFunction)
        {
            int leftLength = middle - left + 1;
            int rightLength = right - middle;

            T[] leftTemporaryArray = new T[leftLength];
            T[] rightTemporaryArray = new T[rightLength];
            int index_1, index_2;

            for (index_1 = 0; index_1 < leftLength; index_1++)
                leftTemporaryArray[index_1] = ListToMerge[left + index_1];
            for (index_2 = 0; index_2 < rightLength; index_2++)
                rightTemporaryArray[index_2] = ListToMerge[middle + index_2 + 1];

            index_1 = 0;
            index_2 = 0;

            int indexForSorting = left;
            while (index_1 < leftLength && index_2 < rightLength)
            {
                if (comparisonFunction(leftTemporaryArray[index_1], rightTemporaryArray[index_2]) <= 0)
                {
                    ListToMerge[indexForSorting] = leftTemporaryArray[index_1];
                    index_1++;
                }
                else
                {
                    ListToMerge[indexForSorting] = rightTemporaryArray[index_2];
                    index_2++;
                }
                indexForSorting++;
            }

            while (index_1 < leftLength)
            {
                ListToMerge[indexForSorting] = leftTemporaryArray[index_1];
                index_1++;
                indexForSorting++;
            }

            while (index_2 < rightLength)
            {
                ListToMerge[indexForSorting] = rightTemporaryArray[index_2];
                index_2++;
                indexForSorting++;
            }
        }

        public  List<T> SortAndMergeAscending(List<T> ListToSort, int left, int right, Func<T, T, int> comparisonFunction)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                SortAndMergeAscending(ListToSort, left, middle, comparisonFunction);
                SortAndMergeAscending(ListToSort, middle + 1, right, comparisonFunction);
                MergeAscending(ListToSort, left, middle, right, comparisonFunction);
            }
            return ListToSort;
        }
    }


}
