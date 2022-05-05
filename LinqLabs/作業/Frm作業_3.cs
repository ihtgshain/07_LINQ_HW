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
using LinqLabs;
using LinqLabs.作業;

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }
        int flag = 0;
        ClassNoLinqForHW3 units;
        ClassNoLinqForHW3 tens;
        ClassNoLinqForHW3 hundreds;
        private void button4_Click(object sender, EventArgs e)
        {
            AllClear();
            ShowDetail(1);
            int[] nums = { 50, 31, 33, 6, 9, 55, 66, 800, 200, 7 };
            units=new ClassNoLinqForHW3("units");
            tens =new ClassNoLinqForHW3("tens");
            hundreds =new ClassNoLinqForHW3("hundreds");

            foreach (int n in nums)
            {
                if (n < 10) units.Add(n);
                else if(n<100) tens.Add(n);
                else hundreds.Add(n);
            }

            List<ClassNoLinqForHW3> list = new List<ClassNoLinqForHW3> { units, tens, hundreds };

            dataGridView1.DataSource = list;

            foreach(ClassNoLinqForHW3 c in list)
            {
                TreeNode node = treeView1.Nodes.Add($"{c.Group} ({c.Count})");
                for(int i=0;i<c.Count;i++)
                {
                    node.Nodes.Add(c[i].ToString());
                }
            }
        }
        private void button38_Click(object sender, EventArgs e)
        {
            AllClear();
            ShowDetail(2);
            var q = new DirectoryInfo(@"c:\windows").GetFiles()
                .OrderByDescending(n => n.Length).GroupBy(n => n.Length.SortLen());

            dataGridView1.DataSource = q.Select(s => new { Size = s.Key, Count = s.Count() }).ToList();

            var qt = q.Select(s => new { Group = s.Key, Count = s.Count(), Size = s.Select(n => n.Length.CalSize()) }).ToList();
            foreach (var c in qt)
            {
                TreeNode node = treeView1.Nodes.Add($"{c.Group} ({c.Count})");
                foreach (var n in c.Size)
                {
                    node.Nodes.Add(n.ToString());
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            AllClear();
            ShowDetail(3);
            var q = new DirectoryInfo(@"c:\windows").GetFiles()
                .OrderByDescending(n => n.CreationTime.Year).GroupBy(n => n.CreationTime.Year);
            
            dataGridView1.DataSource = q.Select(s => new { Year = s.Key, Count = s.Count() }).ToList(); ;

            var qt = q.Select(s => new { Group = s.Key, Count = s.Count(), Name = s.Select(n => n.Name) }).ToList();
            foreach (var c in qt)
            {
                TreeNode node = treeView1.Nodes.Add($"{c.Group} ({c.Count})");
                foreach (var n in c.Name)
                {
                    node.Nodes.Add(n.ToString());
                }
            }
        }

        NorthwindEntities db = new NorthwindEntities();
        private void button8_Click(object sender, EventArgs e)
        {
            AllClear();
            ShowDetail(4);
            dataGridView2.DataSource = db.Products.ToList();

            var q = db.Products.Where(n => n.UnitPrice != null && n.UnitPrice!=0).OrderBy(n=>n.UnitPrice)
                .AsEnumerable().GroupBy(n => n.UnitPrice.Value.SortPrice());

            dataGridView1.DataSource = q.Select(n => new { level = n.Key, count = n.Count() }).ToList();

            var qt = q.Select(s => new { Group = s.Key, Count = s.Count(), Price = s.Select(n =>$"{n.UnitPrice:C2} : {n.ProductName}" )}).ToList();
            foreach (var c in qt)
            {
                TreeNode node = treeView1.Nodes.Add($"{c.Group} ({c.Count})");
                foreach (var n in c.Price)
                {
                    node.Nodes.Add(n.ToString());
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AllClear();
            ShowDetail(5);
            dataGridView2.DataSource = db.Orders.ToList();

            var q = db.Orders.GroupBy(n=>n.OrderDate.Value.Year).OrderBy(n=>n.Key);

            dataGridView1.DataSource = q.Select(s => new { Year = s.Key, Count = s.Count() }).ToList(); ;

            var qt = q.Select(s => new { Group = s.Key, Count = s.Count(), OrderID = s.Select(n =>n.OrderID),OrderDate=s.Select(n=>n.OrderDate.Value)}  ).ToList();
            foreach (var c in qt)
            {
                TreeNode node = treeView1.Nodes.Add($"{c.Group} ({c.Count})");
                foreach (var n in c.OrderID)
                {
                    node.Nodes.Add($"OrderID = {n}");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AllClear();
            ShowDetail(6);
            dataGridView2.DataSource = db.Orders.ToList();

            var q= db.Orders.AsEnumerable().GroupBy(n=>n.OrderDate.Value.ToString("yyyy年MM月")).OrderBy(n => n.Key);

            dataGridView1.DataSource = q.Select(s => new { Year = s.Key, Count = s.Count() }).ToList(); ;

            var qt = q.Select(s => new { Group = s.Key, Count = s.Count(), OrderID = s.Select(n => n.OrderID), OrderDate = s.Select(n => n.OrderDate.Value) }).ToList();
            foreach (var c in qt)
            {
                TreeNode node = treeView1.Nodes.Add($"{c.Group} ({c.Count})");
                foreach (var n in c.OrderID)
                {
                    node.Nodes.Add($"OrderID = {n}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllClear();
            listBox1.Items.Add($"銷售總金額 = {db.Order_Details.Sum(n => n.UnitPrice * n.Quantity):C2}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllClear();
            ShowDetail(7);
            dataGridView2.DataSource = db.Order_Details.ToList();
            dataGridView1.DataSource = db.Order_Details.AsEnumerable()
                .Select(n => new { Name = n.Order.Employee.FirstName +" "+ n.Order.Employee.LastName, Amount = n.UnitPrice * n.Quantity })
                .GroupBy(g => g.Name).Select(g => new { Name = g.Key, Amount = g.Sum(a => a.Amount)})
                .OrderByDescending(n=>n.Amount).Select(g => new { Name = g.Name, Amount = $"{g.Amount:C2}" }).Take(5).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AllClear();
            dataGridView2.DataSource = db.Products.ToList();
            dataGridView1.DataSource = db.Products.Where(n => n.UnitPrice != null && n.UnitPrice != 0)
                .Select(c => new { CategoryName = c.Category.CategoryName, ProductName = c.ProductName, Price = c.UnitPrice })
                .OrderByDescending(c => c.Price).Take(5).ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AllClear();
            bool result = db.Products.Select(p => p.UnitPrice).Any(p => p > 300);
            listBox1.Items.Add((result ? "有" : "無") + "產品價格大於300");
        }
        public void AllClear()
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            treeView1.Nodes.Clear();
            listBox1.Items.Clear();
            flag = 0;
            lblMaster.Text = "Master";
        }
        private void ShowDetail(int index)
        {
            lblMaster.Text = "Master   (↓點擊各項目顯示詳細內容)";
            flag = index;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string index = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (flag == 1)
            {
                switch (index)
                {
                    case "UNITS":
                        dataGridView2.DataSource = units.nums.Select(n => new { Numbers = n }).ToList();
                        break;
                    case "TENS":
                        dataGridView2.DataSource = tens.nums.Select(n => new { Numbers = n }).ToList();
                        break;
                    case "HUNDREDS":
                        dataGridView2.DataSource = hundreds.nums.Select(n => new { Numbers = n }).ToList();
                        break;
                }
            }
            else if (flag == 2)
            {
                dataGridView2.DataSource = new DirectoryInfo(@"c:\windows").GetFiles()
                    .OrderByDescending(n => n.Length).Where(x => x.Length.SortLen() == index)
                    .Select(s => new { FileName = s.Name, Size = s.Length.CalSize() }).ToList();
            }
            else if (flag == 3)
            {
                dataGridView2.DataSource = new DirectoryInfo(@"c:\windows").GetFiles()
                    .Where(x => x.CreationTime.Year.ToString() == index)
                    .Select(s => new { FileName = s.Name, CreationDate = s.CreationTime.ToString("yyyy.MM.dd") }).ToList();
            }
            else if (flag == 4)
            {
                dataGridView2.DataSource = db.Products.AsEnumerable().Where(n => (n.UnitPrice != null && n.UnitPrice != 0) && n.UnitPrice.Value.SortPrice() == index)
                    .OrderBy(n => n.UnitPrice).Select(s => new { PriceLevel = s.UnitPrice.Value.SortPrice(), s.ProductName, Price = $"{s.UnitPrice:C2}" }).ToList();
            }
            else if (flag == 5)
            {
                dataGridView2.DataSource = db.Orders.AsEnumerable().Where(n => n.OrderDate.Value.Year.ToString() == index)
                    .OrderBy(n => n.OrderDate.Value).Select(s => new { s.OrderDate, s.OrderID, s.Employee }).ToList();
            }
            else if (flag == 6)
            {
                dataGridView2.DataSource = db.Orders.AsEnumerable().Where(n => n.OrderDate.Value.ToString("yyyy年MM月") == index)
                    .OrderBy(n => n.OrderDate.Value).Select(s => new { s.OrderDate, s.OrderID, s.Employee }).ToList();
            }
            else if (flag == 7)
            {
                dataGridView2.DataSource = db.Order_Details.AsEnumerable()
                    .Where(n => n.Order.Employee.FirstName + " " + n.Order.Employee.LastName == index).OrderBy(n => n.OrderID)
                    .Select(n => new { n.OrderID, n.UnitPrice, n.Quantity, Amount = n.UnitPrice * n.Quantity }).ToList();
            }
        }
    }
}


public static class MyExtensionMethod
{
    const int small = 1024;
    const int medium = 1024 * 1024;
    const int large = 1024 * 1024 * 1024;

    public static string SortLen(this long size)
    {    
        if (size < small) return "Small";
        else if (size < medium) return "Medium";
        else if (size < large) return "Large";
        else return "Extreme";
    }
    public static string CalSize(this long size)
    {
        if (size == 0) return $"遠小於 1 Byte";
        else if (size < small) return $"{size} Bytes";
        else if (size < medium) return $"{Math.Round((double)size / small, 2)} KB";
        else if (size < large) return $"{Math.Round((double)size / medium, 2)} MB";
        else return $"{Math.Round((double)size / large, 2)} GB";
    }
    public static string SortPrice(this decimal price)
    {
        if (price < 10) return "cheap";
        else if (price < 100) return "medium";
        else return "expensive";
    }
}

