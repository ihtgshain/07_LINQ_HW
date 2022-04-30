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
    }
}