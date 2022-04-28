using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            productsTableAdapter1.Fill(nwDataSet11.Products);
            ordersTableAdapter1.Fill(nwDataSet11.Orders);
        }
        


        //todo 0427 3 Linq start. The introduce for IEnumerator and IEnumberable<T>
        private void button4_Click(object sender, EventArgs e)
        {
            
            int[] nums = Enumerable.Range(0, 10).ToArray();

            foreach(int n in nums)
            {
                listBox1.Items.Add(n);
            }
            listBox1.Items.Add("=====================================");

            System.Collections.IEnumerator en =nums.GetEnumerator();

            while (en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }
        }
        //todo 0427 4 List ver with using var to declare variable (1.type with longer name 2.anonymous type
        private void button5_Click(object sender, EventArgs e)
        {
            var list = Enumerable.Range(1, 10).ToList();
            foreach (int n in list)
            {
                listBox1.Items.Add(n);
            }

            listBox1.Items.Add("==========================");
            var en = list.GetEnumerator();
            while (en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }
        }


        //todo 0427 5 first LINQ 
        private void button2_Click(object sender, EventArgs e)
        {
            //step 1:define DataSource (from n in nums)
            //ster 2:define Query (where,select)
            //ster 3:execute Query (foreach)

            //q=迭代器 iterator

            var nums = Enumerable.Range(1, 10);
            IEnumerable<int> q = from n in nums
                                 where (n <3 || n > 8) && n%2==0
                                 select n;
            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }

            //var q = Enumerable.Range(1, 10).Where(x => x > 5 && x < 8 && n%2==0).ToArray();
            //foreach (int n in q)
            //    listBox1.Items.Add(n);
        }

        //todo 0427 6 Linq with Method
        private void button6_Click(object sender, EventArgs e)
        {
            var q = Enumerable.Range(1, 30).Where(x => IsEven(x));
            foreach (int n in q)
                listBox1.Items.Add(n);
        }

        bool IsEven(int x)
        {
            return x % 2 == 0;
        }

        //todo 0427 7 Show result through DataSource(DataGridView,Chart)
        private void button7_Click(object sender, EventArgs e)
        {
            //int[] nums = Enumerable.Range(1, 10).ToArray();
            //IEnumerable<Point> q = from n in nums
            //                       where n > 5
            //                       select new Point(n, n * n);

            var q = Enumerable.Range(1, 10).Where(s => s > 5).Select(x => new Point(x, x*x));

            foreach (Point pt in q)
            {
                listBox1.Items.Add(pt.X + ", " + pt.Y);
            }
            //========ToList and====================
            List<Point> list = q.ToList();
            
            //========DataGridView ====================
            dataGridView1.DataSource = list;

            //==============Chart============================
            chart1.DataSource = list;
            chart1.Series[0].XValueMember = "X";  //Point.X
            chart1.Series[0].YValueMembers = "Y";  //Point.Y
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        //todo 0427 8 string and contains 
        private void button1_Click(object sender, EventArgs e)
        {
            string[] names = { "aaa", "aaapple", "Apple", "pine", "pineApple" };
            //var q0 = names.Where(n => n.Contains("Apple"));
            //var q1 = names.Select(n => n.ToUpper()).Where(n => n.Contains("APPLE"));

            var q = names.Where(n => n.ToUpper().Contains("APPLE"));

            foreach(string n in q)
            {
                listBox1.Items.Add(n);
            }

            //=============ToList for DataGridView===========
            dataGridView1.DataSource = q.ToList();  //DGV show the properties in Header   Length is the only proverty of String
        }
        //todo 0427 9 Products(nwDataSet)
        private void button8_Click(object sender, EventArgs e)
        {
            //找不到命名空間時可用global::
            //dataGridView1.DataSource = nwDataSet11.Products;
            //IEnumerable < global::LinqLabs.NWDataSet1.ProductsRow q 
            //    = nwDataSet11.Products.Where(x => !x.IsUnitPriceNull() && x.UnitPrice > 30 && x.ProductName.StartsWith("M"));

            dataGridView1.DataSource = nwDataSet11.Products.Where(x => !x.IsUnitPriceNull() && x.UnitPrice > 30 && x.ProductName.StartsWith("M")).ToList();
        }
        //todo 0427 10 Orders and OrderByDescending
        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nwDataSet11.Orders.Where(x => x.OrderDate.Year == 1997 && x.OrderDate.Month>=1 && x.OrderDate.Month <=3).OrderByDescending(x=>x.OrderDate).ToList();
        }

        private void button49_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
