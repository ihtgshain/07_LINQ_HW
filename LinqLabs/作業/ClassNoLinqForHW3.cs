using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinqLabs.作業
{
    public class ClassNoLinqForHW3
    {
        public List<int> nums=new List<int>();
        public string Group { get; set; }

        public ClassNoLinqForHW3(string s)
        {
            Group= s.ToUpper();
        }

        public void Add(int n)
        {
            nums.Add(n);
            nums.Sort();
        }

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= nums.Count)
                    throw new InvalidOperationException("OutOfRangeException: Index= " + index);
                return nums[index];
            }
        }
        public int Count { get => nums.Count;}
        public int Sum
        {
            get
            {
                int temp = 0;
                foreach (int n in nums)
                    temp += n;
                return temp;
            }
        }
        public Double Avg
        {
            get
            {
                double temp = 0;
                foreach (int n in nums)
                    temp += n;
                return Math.Round(temp / nums.Count,2);
            }
        }
        public int Max
        {
            get
            {
                int temp = int.MinValue;
                foreach (int n in nums)
                    if (n > temp) temp = n;
                return temp;
            }
        }
        public int Min
        {
            get
            {
                int temp = int.MaxValue;
                foreach (int n in nums)
                    if (n < temp) temp = n;
                return temp;
            }
        }
    }
}
