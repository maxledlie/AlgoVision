using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace AlgorithmTests
{
    [TestClass]
    public class TestSortingAlgorithms
    {
        private IList<int> _list;

        [TestInitialize]
        public void SetUp()
        {
            _list = Utility.RandomIntList(10, -10, 10);
        }

        [TestMethod]
        public void TestBubbleSort()
        {
            new BubbleSort().Sort(_list);
            Assert.IsTrue(_list.IsSorted());
        }

        [TestMethod]
        public void TestInsertionSort()
        {
            new InsertionSort().Sort(_list);
            Assert.IsTrue(_list.IsSorted());
        }

        [TestMethod]
        public void TestMergeSort()
        {
            new MergeSort().Sort(_list);
            Assert.IsTrue(_list.IsSorted());
        }

        [TestMethod]
        public void TestQuickSort()
        {
            new QuickSort().Sort(_list);
            Assert.IsTrue(_list.IsSorted());
        }
    }
}
