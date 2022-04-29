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
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
            productsTableAdapter1.Fill(nwDataSet11.Products);
           
        }
        //todo 0428 1 Swap and Type
        private void button4_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;
            
            MessageBox.Show(n1 + ", " + n2);

            Swap(ref n1, ref n2);

            MessageBox.Show(n1 + ", " + n2);

            //===String Ver.
            string s1 = "AAA", s2 = "BBB";

            MessageBox.Show(s1 + ", " + s2);

            Swap(ref s1, ref s2);

            MessageBox.Show(s1 + ", " + s2);

        }

        void Swap(ref int n1, ref int n2)
        {
            int temp = n1;
            n1 = n2;
            n2 = temp;
        }

        void Swap(ref string s1, ref string s2)
        {
            string temp = s1;
            s1 = s2;
            s2 = temp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //int n1 = 100, n2 = 200;

            //MessageBox.Show(n1 + ", " + n2);

            //SwapObj(ref n1, ref n2);

            //MessageBox.Show(n1 + ", " + n2);

            //使用時要轉型，不便。


        }
        //========================Swap(Object)  v1.0
        void SwapObj(ref object s1, ref object s2)
        {
            object temp = s1;
            s1 = s2;
            s2 = temp;
        }
        //=========================Swap(Generic)  v2.0
        private void button7_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;

            MessageBox.Show(n1 + ", " + n2);

            //SwapT<int>(ref n1, ref n2);
            SwapT(ref n1, ref n2);  //<int>省略仍可自動判斷

            MessageBox.Show(n1 + ", " + n2);

            //===String Ver.=====================================================
            string s1 = "AAA", s2 = "BBB";

            MessageBox.Show(s1 + ", " + s2);

            SwapT(ref s1, ref s2);

            MessageBox.Show(s1 + ", " + s2);
        }

        void SwapT<T>(ref T s1, ref T s2)
        {
            T temp = s1;
            s1 = s2;
            s2 = temp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //======以下是C# 1.0=================
            //buttonX.Click += ButtonX_Click;  ( ButtonX_Click的簽名錯誤)
            buttonX.Click += new EventHandler(aaa);
            buttonX.Click += bbb;

  

            //======以下是C# 2.0 anonymous delegate
            buttonX.Click += delegate(object sender1, EventArgs e1){
                MessageBox.Show("Anomyous Method");  //匿名方法適合在：只用一次、且內容短的方法
            };

            //======以下是C# 3.0 lambda  => goes to
            buttonX.Click += (object sender1, EventArgs e1) => /*{*/
                MessageBox.Show("Lambda");  
            /*}*/;

        }

        private void ButtonX_Click()
        {
            MessageBox.Show("ButtonX is Clicked");
        }
        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }
        private void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }

        //todo 0428 2 Delegate
        //step1 create a delegate
        //step2 implement a delegate(method)
        //step3 invoke method by the delegate (invoke can be omitted)
        delegate bool Mydelegate(int n);

        bool test(int n)
        {
            return n > 5;
        }
        bool test1(int n)
        {
            return n % 2 == 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool result=test(4);
            MessageBox.Show("1.result = " + result);

            //======C# 1.0 normal delegate=================
            Mydelegate myDelegte = new Mydelegate(test);
            result=myDelegte(7);

            MessageBox.Show("2.result = " + result);

            //===============================
            myDelegte = test1;
            result = myDelegte(2);
            MessageBox.Show("3.result = " + result);

            //todo 0429 1 delegate and anonymous method and Lambda
            //======C# 2.0 anonymous delegate
            myDelegte = delegate (int n) { return n > 5; };
            result = myDelegte(7);    
            MessageBox.Show("4.result = " + result);  //anonymous method for : one time use and shor code


            //======C# 3.0 lambda  => goes to
            myDelegte = n => n > 8;   //type, {}, return can be omitted
            result = myDelegte(7);
            MessageBox.Show("5.result = " + result);
        }
        //todo 0429 2 use delegate as Argument

        //Normal version
        List<int> myWhere1(int[] nums, Mydelegate theDelegate)
        {
            List<int> list = new List<int>();
            foreach (int n in nums)
            {
                if (theDelegate(n))
                {
                    list.Add(n);
                }
            }
            return list;
        }

        //try to use lambda
        List<int> myWhere2(int[] nums, Mydelegate theDelegate)=> nums.Where(n => theDelegate(n)).ToList();

        
        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = Enumerable.Range(1, 20).ToArray();
            List<int> large_list=myWhere2(nums, test1);
            //foreach(int n in large_list)
            //    listBox1.Items.Add(n);


            //use anonymous delegate with lambda 
            List<int> large_list2 = myWhere2(nums, n => n > 5);
            List<int> odd_list = myWhere2(nums, n => n % 2 == 1);
            List<int> even_list = myWhere2(nums, n => n % 2 == 0);
            //foreach(int n in large_list2)
            //    listBox1.Items.Add(n);

            //var even_list2

            foreach (int n in odd_list)
                listBox1.Items.Add(n);
            foreach (int n in even_list)
                listBox2.Items.Add(n);
        }
        //0429 3 yeild return can return IEnumberable object ========================
        IEnumerable<int> myIterator(int[] nums, Mydelegate theDelegate)
        {
            foreach (int n in nums)
            {
                if (theDelegate(n))
                {
                    yield return n;
                }
            }
        }
        
        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = Enumerable.Range(1, 10).ToArray();
            IEnumerable<int> q = myIterator(nums, n => n > 5);
            //IEnumerable<int> q = nums.Where(n => n > 5);


            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
        }


        //todo 0429 4 LINQ 3.0
        private void button3_Click(object sender, EventArgs e)
        {
            //========int[]==================
            var q = Enumerable.Range(1, 10).Where(n => n > 5);
            foreach (var n in q)
                listBox1.Items.Add(n);

            //========string[]==================
            string[] words = { "aaa", "bbbb", "ccccc" };
            var q1 = words.Where(w => w.Length > 3);
            foreach(var c in q1)
                listBox2.Items.Add(c);

            //============NWDataSet products  price >30
            listBox1.Items.Clear();
            //var q2=nwDataSet11.Products.Where(x => x.UnitPrice > 30).Select(x=>$"{x.ProductName} : {x.UnitPrice}").ToList();
            //foreach (var p in q2)
            //    listBox1.Items.Add(p);

            var q2 = nwDataSet11.Products.Where(x => x.UnitPrice > 30).Select(x => $"{x.ProductName} : {x.UnitPrice}").ToList();
                q2.ForEach(s=>listBox1.Items.Add(s));  //for List only

            var q3 = nwDataSet11.Products.Where(x => x.UnitPrice > 30).ToList();
            dataGridView1.DataSource = q3;


        }

        private void button45_Click(object sender, EventArgs e)
        {
            //try var ======================================================
            //var nf = 100;
            //var d = "string";
            //int len = d.Length;
            //var hs = new HashSet<int>();
            //hs.Add(nf);
        }


        //todo 0429 5 reveiew developping my class 
        private void button41_Click(object sender, EventArgs e)
        {
            MyPoint mp = new MyPoint();
            mp.P1 = 100;
            //MessageBox.Show(mp.P1.ToString());


            List<MyPoint> list = new List<MyPoint>();

            //==========implement with initializer
            list.Add(new MyPoint());
            list.Add(new MyPoint("abc"));
            list.Add(new MyPoint(12));
            list.Add(new MyPoint(44, 55));

            //==========implement with {} supported by Intellisense
            list.Add(new MyPoint() { P1 = 100, P2 = 200, S1 = "111", S2 = "BB" });
            list.Add(new MyPoint() { P1 = 20 });
            list.Add(new MyPoint() { P1 = 44, P2 = 50, S1 = "aaa", S2 = "ccc" });

            dataGridView1.DataSource = list;

            //==========implement with {} ver.2
            List<MyPoint> list2 = new List<MyPoint>
            {
                new MyPoint{P1=1,P2=2,S1="aaa",S2="bbb"},
                new MyPoint{P1=11,P2=2,S1="aaa",S2="bbb"},
                new MyPoint{P1=111,P2=2,S1="aaa",S2="bbb"},
                new MyPoint{P1=1111,P2=2,S1="aaa",S2="bbb"}
            };
            dataGridView2.DataSource = list2;
        }

        //=======overload constructor/initializer
        public class MyPoint
        {
            public MyPoint()
            {

            }
            public MyPoint(string s1)
            {

            }
            public MyPoint(string s1,string s2)
            {

            }
            public MyPoint(int p1)
            {
                this.P1 = p1;
            }
            public MyPoint(int p1,int p2)
            {
                this.P1 = p1;
                this.P2 = p2;
            }

            private int _p1;
            public int P1
            {
                get
                {
                    //logic...
                    return _p1;
                }
                set
                {
                    //logic...
                    _p1 = value;
                }
            }
            public int P2 { get; set; }
            public string S1="aaa";
            public string S2 = "BBB";
        }
        //todo 0429 6 anonymous Type (perproties is unlimited
        private void button43_Click(object sender, EventArgs e)
        {
            var x = new { P1 = 22, P2 = 33, P3 = 77 };  //system will automatically create a type name
            var y = new { P1 = 44, P2 = 55, P3 = 33 };  // because of the same paraments with x, y will get the same type name
            var z = new { userName = "aaa", password="bbb" };  //z has different paraments so it will get a different type name
            listBox1.Items.Add(x.GetType()); //to see its type name
            listBox1.Items.Add(y.GetType());
            listBox1.Items.Add(z.GetType());
        }
        // todo 0429 7 use anonymous Type to deal with nwDataSet
        private void button40_Click(object sender, EventArgs e)
        {
            //int[] nums = Enumerable.Range(1,10).ToArray();
            ////var q = from n in nums
            ////        where n > 5
            ////        select new { N = n, S = n * n, C = n * n * n };
            //var q = nums.Where(n => n > 5).Select(n => new { N = n, S = n * n, C = n * n * n });  
            //1 line = expression    multiple lines = {statement}  (need {})

            dataGridView1.DataSource = Enumerable.Range(1, 10).ToArray().Where(n => n > 5).Select(n => new { N = n, S = n * n, C = n * n * n }).ToList();
            
            dataGridView2.DataSource = nwDataSet11.Products.Where(n => n.UnitPrice > 30).                                          
                Select(p => new { ID = p.ProductID, Name = p.ProductName, p.UnitsInStock, p.UnitPrice
                , TotalPrice = $"{p.UnitPrice * p.UnitPrice:C2}"}).ToList();
                                       //string formate is available
        }

        // todo 0429 8 extensionMethod
        private void button32_Click(object sender, EventArgs e)
        {
            string s1 = "aaaaa";
            MessageBox.Show(s1+" wordCount= " + s1.wordCount());

            string s2 = "123456789";
            MessageBox.Show(s2 + " wordCount= " + s2.wordCount());

            string s3="abcdefghi";
            MessageBox.Show(s3 + "ch of 3 = " + s3.chars(3));
        }
    }

    //extension method must be put in namespace or out of namespace (don't put in a class)
    public static class MyStringExtention  //public static is necessary 
    {
        public static int wordCount(this string s) //public static is necessary again
        {                           //"this" stands for which invoke this method and can't be omited !!
            return s.Length;
        }

        public static char chars(this string s, int index) //adding a extra parameter is abailable !!
        {
            return s[index];
        }
    }
}
