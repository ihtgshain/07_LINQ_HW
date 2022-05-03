using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(nwDataSet11.Orders);

        }

        //0502 2 Group with TreeView ListView
        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = Enumerable.Range(1, 20).ToArray();
            //var q1 = from n in nums
            //        group n by n % 2;
            //dataGridView1.DataSource = q1.ToList();
            dataGridView1.DataSource=nums.GroupBy(n => n % 2 == 0 ? "偶數" : "奇數").ToList();
            //DataGridView can't show the elements in group because it has no property.
            //Therefore, we need TreeView or ListView

            //TreeView===============================
            treeView1.Nodes.Clear();
            IEnumerable<IGrouping<string,int>> q = nums.GroupBy(n => n % 2 ==0? "偶數":"奇數");
            foreach(var group in q)
            {
                TreeNode node = treeView1.Nodes.Add(group.Key.ToString());
                foreach(var item in group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }


            //ListView================================
            foreach (var group in q)
            {                                            //key     headerText
                ListViewGroup lvg = listView1.Groups.Add(group.Key,group.Key);
                foreach (var item in group)
                {
                    listView1.Items.Add(item.ToString()).Group=lvg;  
                                                        //remark: use "Group" as Items'property and assign it to lvg
                }
            }
        }

        //todo 0502 3 Group and Aggregation
        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = Enumerable.Range(1, 20).ToArray();
            //var q1 = from n in nums
            //         group n by n % 2 into d
            //         select d=> new { MyKey = d.Key, MyMin = d.Min(), MyMax = d.Max(), MyCount = d.Count() };
            //dataGridView1.DataSource = q1.ToList();

            var q = nums.GroupBy(n => n % 2 == 0 ? "偶數" : "奇數")
                .Select(n => new { MyKey = n.Key, MyMin = n.Min(), MyMax = n.Max(), MyCount = n.Count() , MyGroup = n });
            dataGridView1.DataSource = q.ToList();

            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string header = $"{group.MyKey}({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyKey.ToString(),header);  //header is optional
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }

        //todo 0502 4 Group with MyMethod and chart
        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = Enumerable.Range(1, 21).ToArray();
            var q = nums.GroupBy(n => MyMethod(n))
                .Select(n => new { MyKey = n.Key, MyMin = n.Min(), MyMax = n.Max(), MyCount = n.Count(),MyAvg=n.Average(), MyGroup = n }).ToList();
            dataGridView1.DataSource = q;//DataGridView can't show the elements in group

            treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string header = $"{group.MyKey}({group.MyCount})";
                TreeNode node = treeView1.Nodes.Add(group.MyKey.ToString(), header);  //header is optional
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //chart===============================
            chart1.DataSource = null;
            chart1.DataSource = q;
            chart1.Series[0].XValueMember = "MyKey";
            chart1.Series[0].YValueMembers = "MyCount";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[1].XValueMember = "MyKey";
            chart1.Series[1].YValueMembers = "MyAvg";
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }
        private string MyMethod(int n) 
        {
            if (n <= 7)
                return "small";
            else if (n <= 14)
                return "medium";
            else
                return "large";
        }
        

        //todo 0502 5 FileInfo[] 
        private void button31_Click(object sender, EventArgs e)
        {
            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            //FileInfo[] file = dir.GetFiles();
            //var q= file.GroupBy(n => n.Extension).Select(n => new {MyExt=n.Key , Mycount=n.Count()}).ToList();
            //dataGridView1.DataSource = q;

            dataGridView1.DataSource= new DirectoryInfo(@"c:\windows").GetFiles()
                .GroupBy(n => n.Extension).Select(n => new { MyExt = n.Key, Mycount = n.Count() }).ToList();
        }

        //todo 0502 6 nwDataset
        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = nwDataSet11.Orders.GroupBy(n => n.OrderDate.Year)
                .OrderByDescending(y => y.Key).Select(x => new { Year = x.Key, Count = x.Count() }).ToList();

            MessageBox.Show("1997年訂單數量= " + nwDataSet11.Orders.Where(n => n.OrderDate.Year == 1997).Count());
        }

        // todo 0502 7 how to use let
        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            FileInfo[] file = dir.GetFiles();
            var q = file.Where(x=>x.Extension==".exe").Count();
            //let can define a new name for item's property
            //however it can be replaced by select(n=>new{A=n.XXX})
            
            MessageBox.Show("Count = "+q);
        }

        //todo 0502 8 deal with string {}
        private void button5_Click(object sender, EventArgs e)
        {
            string s = "this is a book. this is a pan, and      this is a apple.";
            char[] chars = { ',', '.', ' ', '?' };
            string[] words=s.Split(chars,StringSplitOptions.RemoveEmptyEntries);//filter off space("")

            var q=words.GroupBy(n => n.ToUpper()).Select(n => new { Word = n.Key, Count = n.Count() }).ToList();
            dataGridView1.DataSource = q;
        }
        // todo 0502 9 minor method 
        private void button15_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 1, 3, 5, 7, 8, 77, 3};
            int[] nums2 = { 1, 2, 5, 7, 9, 66,111};

            //Dictinct Union Intersect Except==================
            IEnumerable<int> q;
            q = nums1.Union(nums2);
            q = nums1.Distinct();
            q = nums1.Intersect(nums2);//set a breakPoint to see them.

            //Any All Contains (return bool)==========================
            bool result;
            result = nums1.Any(n => n >100);
            result = nums2.Any(n => n >100);
            result = nums1.All(n => n > 2);
            result = nums2.Any(n => n >=1);

            //Take Takewhile Skip Skipwhile====================
            //skip


            //First Last Single ElementAt
            //FirstOrDefault LastOrDefault SingleOrDefault ElementAtOrDefault
            int n1;
            n1 = nums1.First();
            n1 = nums1.Last();
            //n1 = nums1.ElementAt(100);  out of range exception
            n1 = nums1.ElementAtOrDefault(100);
            //n1=nums1.Single()  if the array has elements, exception will happen.


            //ToList ToArray ToDictionary ToLookUP AsEnumerable AsQuerable======================
            //skip


            //Range Range Repeat Empty DefaultEmpty=============
            var q1 = Enumerable.Range(1, 100).Select(n => new { n }).ToList();
            dataGridView1.DataSource = q1;

            var q2 = Enumerable.Repeat(60, 100).Select(n => new { N=n }).ToList();
            dataGridView2.DataSource = q2;


            //ConCat SeQuenceEqual==============================
        }
        //todo 0502 10 join 
        private void button10_Click(object sender, EventArgs e)
        {
            //productsTableAdapter1.Fill(nwDataSet11.Products);
            //categoriesTableAdapter.Fill(nwDataSet11.Categories);

            //var q = nwDataSet11.Products.GroupBy(c => c.CategoryID).Select(c => new { c.Key, Avg=c.Average(n => n.UnitPrice) }).ToList();
            //dataGridView1.DataSource = q;

            
            //var q = this.nwDataSet11.Orders.GroupBy(o => o.OrderDate.Year, (key, g) => new { MyKey = key, MyCount = g.Count() });

            var q = from p in this.nwDataSet11.Products
                    group p by p.CategoryID into g
                    select new { CategoryID = g.Key, MyAvg = g.Average(p => p.UnitPrice) };

            this.dataGridView1.DataSource = q.ToList();

            //===================
            //太T-SQL

            var q2 = from c in this.nwDataSet11.Categories
                     join p in this.nwDataSet11.Products
                     on c.CategoryID equals p.CategoryID
                     group p by c.CategoryName into g
                     select new { CategoryName = g.Key, MyAvg = g.Average(p => p.UnitPrice) };

            this.dataGridView2.DataSource = q2.ToList();

        }





    }
}
