using System;
using System.Collections.Generic;

namespace RollingMedian.Lib
{
    public class Heap<T> where T : IComparable<T>
    {
        private List<T> _elements { get; set; } = new List<T>();

        //heap comparison rule
        //by convention, this heap will enforce _rule(parent, child) = true
        private Func<T, T, bool> _rule { get; set; } = (a, b) => a.CompareTo(b) < 0;

        public Heap() { }
        public Heap(Func<T, T, bool> comparisonRule)
        {
            _rule = comparisonRule;
        }

        public void Add(T value)
        {
            _elements.Add(value);
            int child = _elements.Count - 1;
            int parent = (child - 1) / 2;
            //while the new element is smaller than its parent, bubble it up
            while (parent >= 0 && CheckRule(child, parent))
            {
                Swap(parent, child);
                child = parent;
                parent = (child - 1) / 2;
            }
        }

        public T ExtractMin()
        {
            var min = _elements[0];
            _elements[0] = _elements[_elements.Count - 1];
            _elements.RemoveAt(_elements.Count - 1);
            int parent = 0;
            int child1 = 1;
            int child2 = 2;
            //while the root is bigger than its children, bubble it down
            while ((_elements.Count > child1 && CheckRule(child1, parent))
                || (_elements.Count > child2 && CheckRule(child2, parent)))
            {
                // if both children are available, return the smaller child
                int nextIndex = _elements.Count > child2 && CheckRule(child2, child1) ? child2 : child1;
                Swap(parent, nextIndex);
                parent = nextIndex;
                child1 = parent * 2 + 1;
                child2 = parent * 2 + 2;
            }

            return min;
        }

        private void Swap(int index1, int index2)
        {
            var temp = _elements[index1];
            _elements[index1] = _elements[index2];
            _elements[index2] = temp;
        }

        private bool CheckRule(int index1, int index2)
            => _rule(_elements[index1], _elements[index2]);

        public T PeekMin() => _elements[0];

        public void AddAll(IEnumerable<T> items)
        {
            foreach (var item in items) Add(item);
        }

        public int Count => _elements.Count;
    }
}