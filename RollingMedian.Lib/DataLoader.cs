using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RollingMedian.Lib
{
    public static class DataLoader
    {
        public static List<int> LoadData(string inputFile)
        {
            var data = new List<int>();
            foreach (var line in File.ReadLines(inputFile))
            {
                var values = line.Split('\t', ' ')
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .Select(int.Parse)
                    .ToList();
                data.Add(values[0]);
            }
            return data;
        }
    }
}