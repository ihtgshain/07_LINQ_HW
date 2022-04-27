using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(nwDataSet11.Orders);            
            comboBox1.DataSource = nwDataSet11.Orders.Select(x=>x.OrderDate.Year).Distinct().ToArray();
            button1.Click += ChooseYear;
            comboBox1.SelectedValueChanged += ChooseYear;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new DirectoryInfo(@"c:\windows").GetFiles()
                .Where(x=>x.Extension==".log").ToArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new DirectoryInfo(@"c:\windows").GetFiles()
                .Where(x => x.CreationTime.Year == 2022).ToArray();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new DirectoryInfo(@"c:\windows").GetFiles()
                .Where(x => x.Length >1024*1024 ).ToArray();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource=nwDataSet11.Orders;
        }

        private void ChooseYear(object sender, EventArgs e)
        {
            dataGridView2.DataSource = nwDataSet11.Orders
                .Where(x => x.OrderDate.Year.ToString() == comboBox1.Text).ToArray();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
        }

    }
}
