using System;
using System.Collections.Generic;

namespace ViewModels
{
    public class MergeSort : ObservableSortingAlgorithm
    {
        public override string Name => "Merge Sort";

        public override void Sort<T>(T[] array)
        {
            Sort(array, 0, array.Length - 1);
        }

        private void Sort<T>(IList<T> array, int l, int r) where T : IComparable
        {
            if (l < r)
            {
                // Find the middle point 
                int m = (l + r) / 2;

                // Sort first and  
                // second halves 
                Sort(array, l, m);
                Sort(array, m + 1, r);

                // Merge the sorted halves 
                Merge(array, l, m, r);
            }
        }

        private void Merge<T>(IList<T> array, int l, int m, int r) where T : IComparable
        {
            // Find sizes of two subarrays 
            // to be merged 
            int n1 = m - l + 1;
            int n2 = r - m;

            // Create temp arrays  
            T[] L = new T[n1];
            T[] R = new T[n2];

            // Copy data to temp arrays 
            int i, j;
            for (i = 0; i < n1; ++i)
                L[i] = array[l + i];

            for (j = 0; j < n2; ++j)
                R[j] = array[m + 1 + j];

            // Merge the temp arrays  

            // Initial indexes of first 
            // and second subarrays 
            i = 0;
            j = 0;

            // Initial index of merged 
            // subarry array 
            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i].CompareTo(R[j]) <= 0)
                {
                    array[k] = L[i];
                    UpdateProgress();
                    i++;
                }

                else
                {
                    array[k] = R[j];
                    UpdateProgress();
                    j++;
                }
                k++;
            }

            // Copy remaining elements  
            // of L[] if any  
            while (i < n1)
            {
                array[k] = L[i];
                UpdateProgress();
                i++;
                k++;
            }

            // Copy remaining elements 
            // of R[] if any  
            while (j < n2)
            {
                array[k] = R[j];
                UpdateProgress();
                j++;
                k++;
            }
        }
    }
}
