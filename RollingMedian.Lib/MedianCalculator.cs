using System.Collections.Generic;

namespace RollingMedian.Lib
{
    public static class MedianCalculator
    {
        public static List<int> GetMedians(List<int> data)
        {
            var medians = new List<int>();
            //maxheap for the smaller elements
            var smallHalf = new Heap<int>((p,c) => p > c);
            //minheap for the larger elements
            var bigHalf = new Heap<int>();

            foreach(var value in data)
            {

            }

            return medians;
        }
    }
}