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
        bool flag = false;
        public Frm作業_1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(nwDataSet11.Orders);
            order_DetailsTableAdapter1.Fill(nwDataSet11.Order_Details);
            productsTableAdapter1.Fill(nwDataSet11.Products);
            button1.Click += ChooseYear;
            comboBox1.SelectedValueChanged += ChooseYear;
            button12.Enabled = button13.Enabled = false;
            dataGridView1.CellClick += DataGridView1_CellClick1;
            flag = true;
            textBox1.KeyPress += (object sender, KeyPressEventArgs e) =>
             {
                 if (e.KeyChar != '\b' && (e.KeyChar < '0' || e.KeyChar > '9')) e.Handled = true;
             };
        }

        private void DataGridView1_CellClick1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.DataSource = nwDataSet11.Order_Details
                .Where(x => x.OrderID.ToString() == nwDataSet11.Orders.Rows[e.RowIndex][0].ToString()).ToArray();
            lblDetails.Text = "訂單明細";
        }

        private void cancelOrdersDelegate()
        {
            lblDetails.Text = "無資料";
            lblMaster.Text = "檔案";
            dataGridView1.CellClick -= DataGridView1_CellClick1;
            dataGridView2.DataSource = null;
            flag = false;
        }
        private void doOrdersDelegate()
        {
            lblDetails.Text = "訂單明細";
            lblMaster.Text = "訂單";
            button12.Enabled = button13.Enabled = false;

            if (!flag)
            {
                dataGridView1.CellClick += DataGridView1_CellClick1;
                flag = true;
            }
        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            comboBox1.DataSource = nwDataSet11.Orders.Select(x => x.OrderDate.Year).Distinct().ToArray();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new DirectoryInfo(@"c:\windows").GetFiles()
                .Where(x => x.Extension == ".log").ToArray();
            cancelOrdersDelegate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new DirectoryInfo(@"c:\windows").GetFiles()
                .Where(x => x.CreationTime.Year == 2022).ToArray();
            cancelOrdersDelegate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = new DirectoryInfo(@"c:\windows").GetFiles()
                .Where(x => x.Length > 1024 * 1024).ToArray();
            cancelOrdersDelegate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nwDataSet11.Orders;
            doOrdersDelegate();
        }

        private void ChooseYear(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nwDataSet11.Orders
                .Where(x => x.OrderDate.Year.ToString() == comboBox1.Text).ToArray();
            doOrdersDelegate();
        }

        int current = 0;
        int page = 10;
        int count = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            page = textBox1.Text == "" ? 10 : int.Parse(textBox1.Text);
            current = page;
            dataGridView2.DataSource = nwDataSet11.Products.Take(page).ToArray();
            lblDetails.Text = "產品";
            count = nwDataSet11.Products.Rows.Count;
            button12.Enabled = button13.Enabled = true;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            page = textBox1.Text == "" ? 10 : int.Parse(textBox1.Text);
            dataGridView2.DataSource = nwDataSet11.Products
                .Where(x => !(x.IsCategoryIDNull() || x.IsQuantityPerUnitNull() || x.IsReorderLevelNull() || x.IsSupplierIDNull() ||
                              x.IsUnitPriceNull() || x.IsUnitsInStockNull() || x.IsUnitsOnOrderNull() || x.IsUnitsOnOrderNull()))
                .Skip(current - 2 * page).Take(page).ToArray();
            current -= page;
            if (current <= 0) current = page;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            page = textBox1.Text == "" ? 10 : int.Parse(textBox1.Text);
            dataGridView2.DataSource = nwDataSet11.Products
                .Where(x => !(x.IsCategoryIDNull() || x.IsQuantityPerUnitNull() || x.IsReorderLevelNull() || x.IsSupplierIDNull() ||
                              x.IsUnitPriceNull() || x.IsUnitsInStockNull() || x.IsUnitsOnOrderNull() || x.IsUnitsOnOrderNull()))
                .Skip(current).Take(page).ToArray();
            current += page;
            if (current + page >= count) current = count - page - 1;
        }


    }
}
