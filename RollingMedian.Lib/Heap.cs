using System;
using System.Collections.Generic;

namespace RollingMedian.Lib
{
    public class Heap<T> where T : IComparable<T>
    {
        private List<T> _elements { get; set; } = new List<T>();

        // number of elements in array
        // also used to find the next element to add to
        private int _size = 0;

        //heap comparison rule
        //by convention, this heap will enforce _rule(parent, child) = true
        private Func<T, T, bool> _rule { get; set; } = (a, b) => a.CompareTo(b) > 0;

        public Heap() { }
        public Heap(Func<T, T, bool> comparisonRule)
        {
            _rule = comparisonRule;
        }

        public void Add(T next)
        {
            _elements.Add(next);
            _size++;
            int child = _size;
            int parent = (_size - 1) / 2;
            //while the new element is bigger than its parent, bubble it up
            while (CheckRule(child, parent))
            {
                Swap(parent, child);
                child = parent;
                parent = (child - 1) / 2;
            }
        }

        public T ExtractMin()
        {
            var min = _elements[0];
            _elements[0] = _elements[_size];
            _elements.RemoveAt(_size);
            _size--;
            int parent = 0;
            int child1 = 1;
            int child2 = 2;
            //while the root is smaller than its children, bubble it down
            while ((_size > child1 && CheckRule(child1, parent))
                || (_size > child2 && CheckRule(child1, parent)))
            {
                // if both children are available, return the smaller child
                int nextIndex = _size > child2 && CheckRule(child1, child2) ? child2 : child1;
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

        private bool CheckRule(int parent, int child) => _rule(_elements[parent], _elements[child]);

        public T CheckMin() => _elements[0];
    }
}