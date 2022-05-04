using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqLabs.作業
{
    public class ClassNoLinqForHW3
    {
        private List<int> _nums=new List<int>();
        public string Name { get; set; }

        public ClassNoLinqForHW3(string s)
        {
            Name= s.ToUpper();
        }

        public void Add(int n)
        {
            _nums.Add(n);
            _nums.Sort();
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= _nums.Count)
                    throw new InvalidOperationException("OutOfRangeException: Index= " + index);
                return _nums[index];
            }
        }
        public int Count { get => _nums.Count;}
        public int Sum
        {
            get
            {
                int temp = 0;
                foreach (int n in _nums)
                    temp += n;
                return temp;
            }
        }
        public Double Avg
        {
            get
            {
                double temp = 0;
                foreach (int n in _nums)
                    temp += n;
                return Math.Round(temp / _nums.Count,2);
            }
        }
        public int Max
        {
            get
            {
                int temp = int.MinValue;
                foreach (int n in _nums)
                    if (n > temp) temp = n;
                return temp;
            }
        }
        public int Min
        {
            get
            {
                int temp = int.MaxValue;
                foreach (int n in _nums)
                    if (n < temp) temp = n;
                return temp;
            }
        }
    }
}
