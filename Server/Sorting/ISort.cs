﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.Server.Sorting
{
    public interface ISorter<T>
    {
        void SortAscending(T[] arrayToSort);

        void SortAscending(List<T> listToSort);

        void SortAscending(T[] arrayToSort, Func<T, T, int> comparisonFunction);
        void SortAscending(List<T> listToSort, Func<T, T, int> comparisonFunction);
    }

}
