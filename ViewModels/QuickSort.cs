using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class QuickSort : ObservableSortingAlgorithm
    {
        public override string Name => "Quick Sort";

        public override void Sort<T>(T[] array)
        {
            Sort(array, 0, array.Length - 1);
        }

        /* This function takes last element as pivot, 
        places the pivot element at its correct 
        position in sorted array, and places all 
        smaller (smaller than pivot) to left of 
        pivot and all greater elements to right 
        of pivot */
        private int Partition<T>(IList<T> arr, int low, int high) where T : IComparable
        {
            T pivot = arr[high];

            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                // If current element is smaller  
                // than or equal to pivot 
                if (arr[j].CompareTo(pivot) <= 0)
                {
                    i++;

                    // swap arr[i] and arr[j] 
                    T temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    UpdateProgress();
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            T temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;
            UpdateProgress();

            return i + 1;
        }

        /* The main function that implements QuickSort() 
        arr[] --> Array to be sorted, 
        low --> Starting index, 
        high --> Ending index */
        private void Sort<T>(IList<T> arr, int low, int high) where T : IComparable
        {
            if (low < high)
            {
                /* pi is partitioning index, arr[pi] is  
                now at right place */
                int pi = Partition(arr, low, high);

                // Recursively sort elements before 
                // partition and after partition 
                Sort(arr, low, pi - 1);
                Sort(arr, pi + 1, high);
            }
        }
    }
}
