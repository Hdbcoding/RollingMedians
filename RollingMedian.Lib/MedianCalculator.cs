using System.Collections.Generic;
using System.Linq;

namespace RollingMedian.Lib
{
    public static class MedianCalculator
    {
        public static List<int> GetMedians(List<int> data)
        {
            var medians = new List<int>();
            //maxheap for the smaller elements
            var smallHalf = new Heap<int>((p, c) => p > c);
            //minheap for the larger elements
            var bigHalf = new Heap<int>();

            foreach (var value in data)
            {
                if (smallHalf.Count == 0 || value < smallHalf.PeekMin()) smallHalf.Add(value); 
                else bigHalf.Add(value);
                BalanceHalves(smallHalf, bigHalf);
                medians.Add(smallHalf.PeekMin());
            }

            return medians;
        }

        private static void BalanceHalves(Heap<int> smallHalf, Heap<int> bigHalf)
        {
            //if there're more items in the small half, pop them over to the big half
            while (smallHalf.Count > bigHalf.Count + 1)
                bigHalf.Add(smallHalf.ExtractMin());
            //if there're more items in the big half, pop them over to the small half
            while (bigHalf.Count > smallHalf.Count)
                smallHalf.Add(bigHalf.ExtractMin());
        }

        public static int SumMedians(List<int> data)
        {
            var medians = GetMedians(data);
            var sum = medians.Aggregate((a, b) => a + b);
            return sum % 10000;
        }
    }

}