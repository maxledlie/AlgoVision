using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class InsertionSort : ObservableSortingAlgorithm
    {
        public override string Name => "Insertion Sort";

        public override void Sort<T>(T[] array)
        {
            int n = array.Length;         
            for (int i = 1; i < n; ++i)
            {
                T key = array[i];
                int j = i - 1;

                // Move elements of array[0..i-1], 
                // that are greater than key,  
                // to one position ahead of 
                // their current position 
                while (j >= 0 && array[j].CompareTo(key) > 0)
                {
                    array[j + 1] = array[j];
                    UpdateProgress();
                    j = j - 1;
                }
                array[j + 1] = key;
                UpdateProgress();
            }
        }
    }
}
