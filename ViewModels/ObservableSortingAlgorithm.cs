using System;
using System.Collections.Generic;

namespace ViewModels
{
    public abstract class ObservableSortingAlgorithm : ProgressUpdater
    {
        public abstract string Name { get; }

        public abstract void Sort<T>(T[] array) where T : IComparable;

        public event EventHandler ProgressUpdate;

        protected void UpdateProgress()
        {
            ProgressUpdate?.Invoke(this, null);
        }
    }
}
