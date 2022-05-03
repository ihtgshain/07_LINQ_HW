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
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
            productsTableAdapter1.Fill(nwDataSet11.Products);
        }

        // 0429 9 collections → 非泛型(沒有<T>)的ArrayList, DataSet, DataTable...
        private void button30_Click(object sender, EventArgs e)
        {
            //use Cast<T>() or OfType<T>() to convert collections
            System.Collections.ArrayList aList = new System.Collections.ArrayList(){1,2,3,4,5,6,7};

            //int type has no properties, so dataGridView1 shows nothing. use Select to creat property.
            dataGridView1.DataSource=aList.Cast<int>().Where(n => n > 4).Select(n => new { n, Square = n * n }).ToList();
            //dataGridView1.DataSource = aList.OfType<int>().Where(n => n > 4).Select(n => new { n, Square = n * n }).ToList();
            //what's the different between Cast and OfType? Replaceable?
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nwDataSet11.Products.OrderByDescending(n => n.UnitsInStock).Take(5).Select(n => new {n.UnitsInStock}).ToList();
        }


        //todo 0502 1 aggregation function
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int[] nums = Enumerable.Range(1, 10).ToArray();
            //listBox1.Items.Add("Sum= " + nums.Sum());
            //listBox1.Items.Add("Min= " + nums.Min());
            //listBox1.Items.Add("Max= " + nums.Max());
            //listBox1.Items.Add("Avg= " + nums.Average());
            //listBox1.Items.Add("Count= " + nums.Count());
            listBox1.Items.Add("SumEven= " + nums.Where(n => n % 2 == 0).Sum());
            listBox1.Items.Add("MinEven= " + nums.Where(n => n % 2 == 0).Min());
            listBox1.Items.Add("MaxEven= " + nums.Where(n => n % 2 == 0).Max());
            listBox1.Items.Add("AvgEven= " + nums.Where(n => n % 2 == 0).Average());
            listBox1.Items.Add("CountEven= " + nums.Where(n => n % 2 == 0).Count());

            //=========nwDataSet==========================
            //aggregation method with paramater to define which data
            listBox1.Items.Add("UnitsInStock Sum = " + nwDataSet11.Products.Sum(p => p.UnitsInStock));
            listBox1.Items.Add("UnitsInStock Max = " + nwDataSet11.Products.Max(p => p.UnitsInStock));
            listBox1.Items.Add("UnitsInStock Min = " + nwDataSet11.Products.Min(p => p.UnitsInStock));
            listBox1.Items.Add("UnitsInStock Avg = " + nwDataSet11.Products.Average(p => p.UnitsInStock));
        }
    }
}