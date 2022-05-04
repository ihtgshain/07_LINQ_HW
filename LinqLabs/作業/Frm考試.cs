using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();

            ss = new List<Student>()
            {
                new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
            };
        }

        NorthwindEntities db = new NorthwindEntities();
        List<Student> ss;
        List<Student> ss2 = new List<Student>();

        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        string[] cbText1 = {"找出 前面三個 的學員所有科目成績","找出 後面兩個 的學員所有科目成績",
                           "找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績","找出學員 'bbb' 的成績",
                           "找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)",
                           "找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績","數學不及格 ... 是誰"
        };

        private void comboBox1_Click(object sender, EventArgs e)
        {
            button36.Enabled = true;
            comboBox1.DataSource = cbText1;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            AllClear();
            // 共幾個 學員成績 ?	
            listBox1.Items.Add($"資料中總共有{ss.Count()}個學員成續。");

            int index = comboBox1.SelectedIndex;

            switch (index)
            {          // 找出 前面三個 的學員所有科目成績
                case 0: dataGridView1.DataSource = ss.Take(3)
                        .Select(n => new { n.Name, n.Gender, n.Class, Chinese = n.Chi, English = n.Eng, n.Math }).ToList();
                    break;
                case 1:// 找出 後面兩個 的學員所有科目成績	
                    dataGridView1.DataSource = ss.Skip(ss.Count() - 2).Take(2)
                        .Select(n => new { n.Name, n.Gender, n.Class, Chinese = n.Chi, English = n.Eng, n.Math }).ToList();
                    break;
                case 2:// 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績	
                    dataGridView1.DataSource = ss.Where(n => n.Name == "aaa" || n.Name == "bbb" || n.Name == "ccc")
                        .Select(n => new { n.Name, Chinese = n.Chi, English = n.Eng }).ToList();
                    break;
                case 3:// 找出學員 'bbb' 的成績	
                    dataGridView1.DataSource = ss.Where(n => n.Name == "bbb")
                     .Select(n => new { n.Name, Chinese = n.Chi, English = n.Eng }).ToList();
                    break;
                case 4:// 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
                    dataGridView1.DataSource = ss.Where(n => n.Name != "bbb")
                        .Select(n => new { n.Name, Chinese = n.Chi, English = n.Eng }).ToList();
                    break;
                case 5:// 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績
                    dataGridView1.DataSource = ss.Where(n => n.Name == "aaa" || n.Name == "bbb" || n.Name == "ccc")
                        .Select(n => new { n.Name, Chinese = n.Chi, n.Math }).ToList();
                    break;
                case 6:// 數學不及格 ... 是誰
                    dataGridView1.DataSource = ss.Where(n => n.Math < 60)
                        .Select(n => new { n.Name, Chinese = n.Chi, n.Math }).ToList();
                    break;
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {            //個人 sum, min, max, avg
            AllClear();
            listBox1.Items.Add($"資料中總共有{ss.Count()}個學員成續。");
            dataGridView1.DataSource = ss.Select(n => new { n.Name, n.Gender, n.Class, Sum = n.Eng + n.Chi + n.Math
                , Max = max(n.Chi, n.Eng, n.Math), Min = min(n.Chi, n.Eng, n.Math), Avg = $"{(double)(n.Eng + n.Chi + n.Math) / 3:F2}" }).ToList();
        }
        private int max(int s1, int s2, int s3)
        {
            int result = s1;
            if (s2 > s1) result = s2;
            if (s3 > s2) result = s3;
            return result;
        }

        private int min(int s1, int s2, int s3)
        {
            int result = s1;
            if (s2 < s1) result = s2;
            if (s3 < s2) result = s3;
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {           //各科 sum, min, max, avg
            AllClear();
            listBox1.Items.Add($"資料中總共有{ss.Count()}個學員成續。");

            var l1 = Enumerable.Range(1, 1).Select(s =>
            new { Subject = "Chinese", Sum = ss.Sum(n => n.Chi), Min = ss.Min(n => n.Chi),
                Max = ss.Max(n => n.Chi), Avg = $"{ss.Average(n => n.Chi):F2}" }).ToList();
            var l2 = Enumerable.Range(1, 1).Select(s =>
            new { Subject = "English", Sum = ss.Sum(n => n.Eng), Min = ss.Min(n => n.Eng),
                Max = ss.Max(n => n.Eng), Avg = $"{ss.Average(n => n.Eng):F2}" }).ToList();
            var l3 = Enumerable.Range(1, 1).Select(s =>
            new { Subject = "Math", Sum = ss.Sum(n => n.Math), Min = ss.Min(n => n.Math),
                Max = ss.Max(n => n.Math), Avg = $"{ss.Average(n => n.Math):F2}" }).ToList();
            l1.Add(l2[0]);
            l1.Add(l3[0]);
            dataGridView1.DataSource = l1;
        }
        Random rd = new Random();
        private void button33_Click(object sender, EventArgs e)
        {   // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
            ss2.Clear();
            AllClear();
            for (int i = 0; i < 100; i++)
            {
                ss2.Add(new Student { Name = "ST-" + Numbering(i + 1), Chi = rd.Next(50, 101) });
            }
            dataGridView1.DataSource = ss2.
                Select(s => new {s.Name, Level = SortGrade(s.Chi), Chinese = s.Chi }).
                OrderByDescending(s => s.Chinese).ToList();
        }

        private string Numbering(int i)
        {
            if (i < 10) return "00" + i;
            else if (i < 100) return "0" + i;
            else return "" + i;
        }

        private string SortGrade(int score)
        {
            if (score < 60) return "不級格";
            else if (score < 70) return "待加強";
            else if (score < 90) return "佳";
            else return "優良";
        }

        private void button35_Click(object sender, EventArgs e)
        {// 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            #region
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
            #endregion
            if (ss2.Count == 0)
            {
                MessageBox.Show("請先按鈕產生100名隨機的學生國文成績");
                return;
            }
            AllClear();
            dataGridView1.DataSource=ss2.OrderByDescending(s => s.Chi).GroupBy(s=>s.Chi)
                .Select(s => new { Score = s.Key,Count=s.Count(),Percentage=$"{(double)s.Count() / ss2.Count()*100:F2}%"}).ToList();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            ShowSellsData();
        }

        private void ShowSellsData()
        {
            AllClear();
            var q = db.Order_Details.AsEnumerable().
                Select(y => new { Year = y.Order.OrderDate.Value.Year, Amount = y.UnitPrice * y.Quantity })
                .GroupBy(n => n.Year).Select(g => new { Key = g.Key, Amount = $"{g.Sum(a => a.Amount):C2}" }).OrderBy(n => n.Amount);

            // 年度最高銷售金額 年度最低銷售金額
            listBox1.Items.Add("年度最高銷售金額：" + q.Last().Amount);
            listBox1.Items.Add("年度最低銷售金額：" + q.First().Amount);
            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
            listBox1.Items.Add("那一年總銷售最好：" + q.Last().Key + " 年");
            listBox1.Items.Add("那一年總銷售最差：" + q.First().Key + " 年");

            var q1 = db.Order_Details.AsEnumerable().
                Select(y => new { Month = y.Order.OrderDate.Value.Year + " 年" + y.Order.OrderDate.Value.Month + " 月", Amount = y.UnitPrice * y.Quantity })
                .GroupBy(n => n.Month).Select(g => new { Key = g.Key, Amount = $"{g.Sum(a => a.Amount):C2}" }).OrderBy(n => n.Amount);

            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?
            listBox1.Items.Add("那一個月總銷售最好：" + q1.Last().Key);
            listBox1.Items.Add("那一個月總銷售最差：" + q1.First().Key);

            // 每年 總銷售分析 圖
            chart1.DataSource = q.ToList();
            chart1.Series[0].XValueMember = "Key";
            chart1.Series[0].YValueMembers = "Amount";
            chart1.Series[0].ChartType = SeriesChartType.Column;
            // 每月 總銷售分析 圖
            chart2.DataSource = q1.ToList();
            chart2.Series[0].XValueMember = "Key";
            chart2.Series[0].YValueMembers = "Amount";
            chart2.Series[0].ChartType = SeriesChartType.Column;
        }

        private void button6_Click(object sender, EventArgs e)
        {//年 銷售成長率
            //我的LINQ有點進步啦!!!
            ShowSellsData();
            var q = db.Order_Details.AsEnumerable().
                Select(y => new { Year = y.Order.OrderDate.Value.Year, Amount = y.UnitPrice * y.Quantity })
                .GroupBy(n => n.Year).Select(g => new { Key = g.Key, Amount = g.Sum(a => a.Amount) })
                .OrderBy(n => n.Key);

            dataGridView1.DataSource = q.Skip(1).Zip(q.Take(q.Count() - 1), (x1, x2) => 
                new { Year = x1.Key, GrowthRate = GRate(x1.Amount, x2.Amount) }).ToList();
        }

        private string GRate(decimal x1,decimal x2)
        {
            return $"{(x1 - x2) / x2 * 100:F2} %";
        }

        private void AllClear()
        {
            chart1.DataSource = null;
            chart2.DataSource = null;
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            listBox1.Items.Clear();
            dataGridView1.DataSource = null;
        }
    }
}
