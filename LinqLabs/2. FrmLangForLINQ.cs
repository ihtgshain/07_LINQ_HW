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
           
        }
        //todo 0428 1 Swap and Type==============================
        private void button4_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;
            
            MessageBox.Show(n1 + ", " + n2);

            Swap(ref n1, ref n2);

            MessageBox.Show(n1 + ", " + n2);

            //===String Ver.=====================================================
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

  

            //======以下是C# 2.0 anomynous delegate
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

        //todo 0428 2 ========Delegate==================
        //step1 create a delegate
        //step2 implement a delegate(method)
        //step3 invoke method by the delegate
        delegate bool Mydelegate(int n);

        private void button9_Click(object sender, EventArgs e)
        {
            bool b=test(4);
            MessageBox.Show("result = " + b);

            Mydelegate md = new Mydelegate(test);
            b=md(7);

            MessageBox.Show("result = " + b);

            //===============================
            md = test1;
            b = md(2);
            MessageBox.Show("result = " + b);
        }

        bool test(int n)
        {
            return n > 5;
        }
        bool test1(int n)
        {
            return n%2==0;
        }
    }
}
