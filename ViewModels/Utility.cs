using System;
using System.Collections.Generic;

namespace ViewModels
{
    public static class Utility
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var random = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T[] Copy<T>(this T[] arr)
        {
            var copy = new T[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                copy[i] = arr[i];
            }
            return copy;
        }

        public static bool IsSorted<T>(this IList<T> list) where T : IComparable
        {
            for (int i = 0; i < list.Count - 1; i++)
                if (list[i].CompareTo(list[i + 1]) > 0)
                    return false;

            return true;
        }

        public static int[] RandomIntArray(int length, int minValue, int maxValue)
        {
            var random = new Random();
            var ret = new int[length];

            for (int i = 0; i < length; i++)
                ret[i] = random.Next(minValue, maxValue);

            return ret;
        }        
    }
}
