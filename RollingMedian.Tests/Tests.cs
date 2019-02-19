using NUnit.Framework;
using RollingMedian.Lib;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RollingMedian.Tests
{
    public class Tests
    {
        [Test]
        public static void MinHeapSort()
        {
            int[] collection = new[] { 5, 4, 3, 2, 1 };
            Heap<int> minHeap = new Heap<int>();
            minHeap.AddAll(collection);

            int[] sorted = new[] { 1, 2, 3, 4, 5 };
            for (int i = 0; i < sorted.Length; i++)
            {
                Assert.AreEqual(sorted[i], minHeap.ExtractMin());
            }
        }

        [Test]
        public static void MaxHeapSort()
        {
            int[] collection = new[] { 1, 2, 3, 4, 5 };
            Heap<int> minHeap = new Heap<int>((p, c) => p > c);
            minHeap.AddAll(collection);

            int[] sorted = new[] { 5, 4, 3, 2, 1 };
            for (int i = 0; i < sorted.Length; i++)
            {
                Assert.AreEqual(sorted[i], minHeap.ExtractMin());
            }
        }

        [Test, TestCaseSource(typeof(TestCaseFactory), "TestCases")]
        public void CanLoadData(string inputFile, string outputFile)
        {
            List<int> data = DataLoader.LoadData(inputFile);
            //assert that each line produced a node
            int count = File.ReadAllLines(inputFile).Length;
            Assert.AreEqual(data.Count, count);
        }

        [Test, TestCaseSource(typeof(TestCaseFactory), "TestCases")]
        public void CorrectRollingMedian(string inputFile, string outputFile)
        {
            List<int> data = DataLoader.LoadData(inputFile);
            int result = MedianCalculator.SumMedians(data);
            int expected = File.ReadAllLines(outputFile)
                .First()
                .Split('\t', ' ', ',')
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(int.Parse)
                .First();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ForumTest1()
        {
            List<int> data = new[] { 1, 666, 10, 667, 100, 2, 3 }.ToList();
            int result = MedianCalculator.SumMedians(data);
            Assert.AreEqual(142, result);
        }

        [Test]
        public void ForumTest2()
        {
            List<int> data = new[] { 6331, 2793, 1640, 9290, 225, 625, 6195, 2303, 5685, 1354 }.ToList();
            int result = MedianCalculator.SumMedians(data);
            Assert.AreEqual(9335, result);
        }

        [Test]
        public void ForumTest3()
        {
            List<int> data = new[] { 23, 9, 35, 4, 13, 24, 2, 5, 27, 1, 34, 8, 15, 39, 32, 22, 29, 21, 19, 20, 36, 33, 7, 31, 14, 17, 26, 16, 38, 6, 30, 40, 25, 28, 11, 37, 3, 10, 18, 12 }.ToList();
            int result = MedianCalculator.SumMedians(data);
            Assert.AreEqual(717, result);
        }

        [Test]
        public void ForumTest4()
        {
            List<int> data = new[]{ 23, 9, 35, 4, 13, 24, 2, 5, 27, 1, 34, 8 }.ToList();
            int result = MedianCalculator.SumMedians(data);
            Assert.AreEqual(156, result);
        }

        [Test]
        public void ExtractMinBubbleRight()
        {
            var data = new[]{ 13, 5, 9, 2, 1, 4, 8 };
            var maxHeap = new Heap<int>((p,c) => p > c);
            maxHeap.AddAll(data);
            Assert.AreEqual(13, maxHeap.ExtractMin());
            Assert.AreEqual(9, maxHeap.ExtractMin());
            Assert.AreEqual(8, maxHeap.ExtractMin());
        }
    }
}