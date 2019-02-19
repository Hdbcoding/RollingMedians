using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RollingMedian.Lib;

namespace RollingMedian.Tests
{
    public class Tests
    {
        [Test]
        public static void MinHeapSort(){
            var collection = new [] {5, 4, 3, 2, 1};
            var minHeap = new Heap<int>();
            minHeap.AddAll(collection);

            var sorted = new[]{ 1, 2, 3, 4, 5 };
            for (int i = 0; i < sorted.Length; i++){
                Assert.AreEqual(sorted[i], minHeap.ExtractMin());
            }
        }

        [Test]
        public static void MaxHeapSort(){
            var collection = new [] {1, 2, 3, 4, 5};
            var minHeap = new Heap<int>((p, c) => p > c);
            minHeap.AddAll(collection);

            var sorted = new[]{ 5, 4, 3, 2, 1 };
            for (int i = 0; i < sorted.Length; i++){
                Assert.AreEqual(sorted[i], minHeap.ExtractMin());
            }
        }

        [Test, TestCaseSource(typeof(TestCaseFactory), "TestCases")]
        public void CanLoadData(string inputFile, string outputFile)
        {
            var data = DataLoader.LoadData(inputFile);
            //assert that each line produced a node
            var count = File.ReadAllLines(inputFile).Length;
            Assert.AreEqual(data.Count, count);
        }
    }
}