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
using LinqLabs.作業;

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 50, 31, 33, 6, 9, 55, 66, 800, 200, 7 };
            ClassNoLinqForHW3 units=new ClassNoLinqForHW3("units");
            ClassNoLinqForHW3 tens =new ClassNoLinqForHW3("tens");
            ClassNoLinqForHW3 hundreds =new ClassNoLinqForHW3("hundreds");

            Array.Sort(nums);
            foreach (int n in nums)
            {
                if (n < 10) units.Add(n);
                else if(n<100) tens.Add(n);
                else hundreds.Add(n);
            }

            List<ClassNoLinqForHW3> list = new List<ClassNoLinqForHW3> { units, tens, hundreds };

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = list;

            treeView1.Nodes.Clear();
            foreach(ClassNoLinqForHW3 c in list)
            {
                TreeNode node = treeView1.Nodes.Add($"{c.Name} ({c.Count})");
                for(int i=0;i<c.Count;i++)
                {
                    node.Nodes.Add(c[i].ToString());
                }
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            var q = new DirectoryInfo(@"c:\windows").GetFiles()
                .OrderByDescending(n => n.CreationTime.Year).GroupBy(n => n.CreationTime.Year);
            
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = q.Select(s => new { Year = s.Key, Count = s.Count() }).ToList(); ;

            var qt = q.Select(s => new { Group = s.Key, Count = s.Count(), Name = s.Select(n => n.Name) }).ToList();

            treeView1.Nodes.Clear();
            foreach (var c in qt)
            {
                TreeNode node = treeView1.Nodes.Add($"{c.Group} ({c.Count})");
                foreach (var n in c.Name)
                {
                    node.Nodes.Add(n.ToString());
                }
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            var q = new DirectoryInfo(@"c:\windows").GetFiles()
                .Where(n => n.Length != 0)
                .OrderByDescending(n => n.Length).GroupBy(n => n.Length.SortLen());

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = q.Select(s => new { Size = s.Key, Count = s.Count() }).ToList();

            var qt =q.Select(s => new { Group = s.Key, Count = s.Count(), Size = s.Select(n => n.Length.CalSize()) }).ToList();

            treeView1.Nodes.Clear();
            foreach (var c in qt)
            {
                TreeNode node = treeView1.Nodes.Add($"{c.Group} ({c.Count})");
                foreach (var n in c.Size)
                {
                    node.Nodes.Add(n.ToString());
                }
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
        if (size < small) return $"{size} Bytes";
        else if (size < medium) return $"{Math.Round((double)size / small,2)} KB";
        else if (size < large) return $"{Math.Round((double)size / medium,2)} MB";
        else return $"{Math.Round((double)size / large,2)} GB";
    }
}

