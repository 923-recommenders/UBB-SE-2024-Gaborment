using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.Sorting
{
    public  class HeapSort<T> : ISorter<T> where T : IComparable<T>
    {
        public  void SortAscending(T[] ArrayToSort) {
            int numberOfElements = ArrayToSort.Length;
            if (numberOfElements > 1)
            { 
                for (int index = numberOfElements / 2 - 1; index >= 0; index--)
                {
                    HeapifyAscending(ArrayToSort, numberOfElements, index);
                }
                for (int index = numberOfElements - 1; index >= 0; index--)
                {
                    var TemporaryVariableForSwitchingValues = ArrayToSort[0];
                    ArrayToSort[0] = ArrayToSort[index];
                    ArrayToSort[index] = TemporaryVariableForSwitchingValues;
                    HeapifyAscending(ArrayToSort, index, 0);
                }
            }
        }

        static void HeapifyAscending(T[] ArrayToHeapify, int numberOfElements, int index)
        {
            int largestIndex = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            if (leftChild < numberOfElements && ArrayToHeapify[leftChild].CompareTo(ArrayToHeapify[largestIndex]) > 0)
            {
                largestIndex = leftChild;
            }
            if (rightChild < numberOfElements && ArrayToHeapify[rightChild].CompareTo(ArrayToHeapify[largestIndex]) > 0)
            {
                largestIndex = rightChild;
            }
            if (largestIndex != index)
            {
                var TemporaryVariable = ArrayToHeapify[index];
                ArrayToHeapify[index] = ArrayToHeapify[largestIndex];
                ArrayToHeapify[largestIndex] = TemporaryVariable;
                HeapifyAscending(ArrayToHeapify, numberOfElements, largestIndex);
            }
        }
        public  void SortAscending(List<T> ListToSort) {
            int numberOfElements = ListToSort.Count;
            if (numberOfElements > 1)
            {
                for (int index = numberOfElements / 2 - 1; index >= 0; index--)
                {
                    HeapifyAscending(ListToSort, numberOfElements, index);
                }
                for (int index = numberOfElements - 1; index >= 0; index--)
                {
                    var TemporaryVariableForSwitchingValues = ListToSort[0];
                    ListToSort[0] = ListToSort[index];
                    ListToSort[index] = TemporaryVariableForSwitchingValues;
                    HeapifyAscending(ListToSort, index, 0);
                }
            }
        }

        static void HeapifyAscending(List<T> ListToHeapify, int numberOfElements, int index)
        {
            int largestIndex = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            if (leftChild < numberOfElements && ListToHeapify[leftChild].CompareTo(ListToHeapify[largestIndex]) > 0)
            {
                largestIndex = leftChild;
            }
            if (rightChild < numberOfElements && ListToHeapify[rightChild].CompareTo(ListToHeapify[largestIndex]) > 0)
            {
                largestIndex = rightChild;
            }
            if (largestIndex != index)
            {
                var TemporaryVariable= ListToHeapify[index];
                ListToHeapify[index] = ListToHeapify[largestIndex];
                ListToHeapify[largestIndex] = TemporaryVariable;
                HeapifyAscending(ListToHeapify, numberOfElements, largestIndex);
            }
        }

        public  void SortAscending(T[] ArrayToSort, Func<T, T, int> comparisonFunction)
        {
            int numberOfElements = ArrayToSort.Length;
            if (numberOfElements > 1)
            {
                for (int index = numberOfElements / 2 - 1; index >= 0; index--)
                {
                    HeapifyAscending(ArrayToSort, numberOfElements, index, comparisonFunction);
                }
                for (int index = numberOfElements - 1; index >= 0; index--)
                {
                    var TemporaryVariableForSwitchingValues = ArrayToSort[0];
                    ArrayToSort[0] = ArrayToSort[index];
                    ArrayToSort[index] = TemporaryVariableForSwitchingValues;
                    HeapifyAscending(ArrayToSort, index, 0, comparisonFunction);
                }
            }
        }

        static void HeapifyAscending(T[] ArrayToHeapify, int numberOfElements, int index, Func<T, T, int> comparisonFunction)
        {
            int largestIndex = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            if (leftChild < numberOfElements && comparisonFunction(ArrayToHeapify[leftChild], ArrayToHeapify[largestIndex]) > 0)
            {
                largestIndex = leftChild;
            }
            if (rightChild < numberOfElements && comparisonFunction(ArrayToHeapify[rightChild], ArrayToHeapify[largestIndex]) > 0)
            {
                largestIndex = rightChild;
            }
            if (largestIndex != index)
            {
                var TemporaryVariable = ArrayToHeapify[index];
                ArrayToHeapify[index] = ArrayToHeapify[largestIndex];
                ArrayToHeapify[largestIndex] = TemporaryVariable;
                HeapifyAscending(ArrayToHeapify, numberOfElements, largestIndex, comparisonFunction);
            }
        }
       

        public  void SortAscending(List<T> ListToSort, Func<T, T, int> comparisonFunction)
        {
            int numberOfElements = ListToSort.Count;
            if (numberOfElements > 1)
            {
                for (int index = numberOfElements / 2 - 1; index >= 0; index--)
                {
                    HeapifyAscending(ListToSort, numberOfElements, index, comparisonFunction);
                }
                for (int index = numberOfElements - 1; index >= 0; index--)
                {
                    var TemporaryVariableForSwitchingValues = ListToSort[0];
                    ListToSort[0] = ListToSort[index];
                    ListToSort[index] = TemporaryVariableForSwitchingValues;
                    HeapifyAscending(ListToSort, index, 0, comparisonFunction);
                }
            }
        }

        static void HeapifyAscending(List<T> ListToHeapify, int numberOfElements, int index, Func<T, T, int> comparisonFunction)
        {
            int largestIndex = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            if (leftChild < numberOfElements && comparisonFunction(ListToHeapify[leftChild], ListToHeapify[largestIndex]) > 0)
            {
                largestIndex = leftChild;
            }
            if (rightChild < numberOfElements && comparisonFunction(ListToHeapify[rightChild], ListToHeapify[largestIndex]) > 0)
            {
                largestIndex = rightChild;
            }
            if (largestIndex != index)
            {
                var TemporaryVariable = ListToHeapify[index];
                ListToHeapify[index] = ListToHeapify[largestIndex];
                ListToHeapify[largestIndex] = TemporaryVariable;
                HeapifyAscending(ListToHeapify, numberOfElements, largestIndex, comparisonFunction);
            }
        }
    }

}
