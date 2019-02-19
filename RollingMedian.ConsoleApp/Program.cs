using RollingMedian.Lib;
using System;

namespace RollingMedian.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = DataLoader.LoadData("medianData.txt");
            var rollingMedian = MedianCalculator.SumMedians(data);
            Console.WriteLine(rollingMedian);
            Console.ReadLine();
        }
    }
}
