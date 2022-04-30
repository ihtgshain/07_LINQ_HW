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
            comboBox2.DataSource = q.Select(x =>x.SDTMonthStr()).Distinct().ToList();
            dateTimePicker1.Value = q.Min();
            dateTimePicker2.Value = q.Max();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = aWdataSet1.Product1;
            textBox1.Text = aWdataSet1.Product1.Count().SCountStr();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker1.Value < dateTimePicker2.Value ? dateTimePicker1.Value : dateTimePicker2.Value;
            DateTime end = dateTimePicker1.Value > dateTimePicker2.Value ? dateTimePicker1.Value : dateTimePicker2.Value;
            var q= aWdataSet1.Product1.Where(x => x.SellStartDate >= start && x.SellStartDate <= end).ToList();
            dataGridView1.DataSource = q;
            textBox1.Text = q.Count().SCountStr();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var q = aWdataSet1.Product1.Where(x => x.SellStartDate.Year.ToString() == comboBox3.Text).ToList();
            dataGridView1.DataSource = q;
            textBox1.Text = q.Count().SCountStr();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var q = aWdataSet1.Product1.
                Where(x => x.SellStartDate.Year.ToString() == comboBox3.Text 
                      && (x.SellStartDate.Month/4+1).ToString()==comboBox2.Text.Substring(1,1)).ToList();
            dataGridView1.DataSource = q;
            textBox1.Text = q.Count().SCountStr();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ID = aWdataSet1.Product1.Rows[e.RowIndex][0].ToString();
            var q = aWdataSet1.IDtoPhoto.Where(i => i.ProductID.ToString() == ID)
                .Select(p => p.LargePhoto).SelectMany(b => b).ToArray();
            pictureBox1.Image=Image.FromStream(new MemoryStream(q));
        }
    }
}
public static class MyExtension
{
    public static string SCountStr(this int n)
    {
        return $"共 {n} 筆";
    }
    public static string SDTMonthStr(this DateTime m)
    {
        return $"第{m.Month / 4 + 1}季";
    }

}





