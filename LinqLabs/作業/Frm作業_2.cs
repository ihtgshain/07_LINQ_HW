using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            Initialization();
        }   

        private void Initialization()
        {   
            product1TableAdapter1.Fill(aWdataSet1.Product1);//omit columns with "allow null" property
            iDtoPhotoTableAdapter1.Fill(aWdataSet1.IDtoPhoto);
            var q = aWdataSet1.Product1.Select(x => x.SellStartDate);
            comboBox3.DataSource = q.Select(x=>x.Year).Distinct().ToList();
            comboBox2.DataSource = q.Select(x =>x.ShowQuarterStr()).Distinct().ToList();
            dateTimePicker1.Value = q.Min();
            dateTimePicker2.Value = q.Max();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = aWdataSet1.Product1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker1.Value < dateTimePicker2.Value ? dateTimePicker1.Value : dateTimePicker2.Value;
            DateTime end = dateTimePicker1.Value > dateTimePicker2.Value ? dateTimePicker1.Value : dateTimePicker2.Value;
            dataGridView1.DataSource = aWdataSet1.Product1
              .Where(x => x.SellStartDate >= start && x.SellStartDate <= end).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = aWdataSet1.Product1
              .Where(x => x.SellStartDate.Year.ToString() == comboBox3.Text).ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = aWdataSet1.Product1.
              Where(x => x.SellStartDate.Year.ToString() == comboBox3.Text
                && (x.SellStartDate.Month / 4+1).ToString() == comboBox2.Text.Substring(1, 1)).ToList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            var byteArr = aWdataSet1.IDtoPhoto.Where(i => i.ProductID.ToString() == ID)
              .Select(p => p.LargePhoto).SelectMany(b => b).ToArray();
            pictureBox1.Image = Image.FromStream(new MemoryStream(byteArr));
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.Rows.Count.ShowCountStr();
        }
    }
}
public static class MyExtension
{
    public static string ShowCountStr(this int n) => $"共 {n} 筆";

    public static string ShowQuarterStr(this DateTime m) => $"第{m.Month / 4 + 1}季";
}





