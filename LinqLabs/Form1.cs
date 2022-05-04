using LinqLabs;
using MyHomeWork;
using Starter;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HomeWork_All
{
    public partial class Form1 : Form
    {
        string[] titleForButton = { "Frm作業1", "Frm作業2", "Frm作業3","Frm考試"};
        Button[] btn ;
        bool created = false;

        public Form1()
        {
            InitializeComponent();
            CreateButton();
        }

        private void CreateButton()
        {
            btn = new Button[titleForButton.Length];
            for (int i = 0; i < btn.Length; i++)
            {
                btn[i] = new Button();
                btn[i].Font = new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(136)));
                btn[i].Location = new Point(19, 1 + i * 70);
                btn[i].Size = new Size(150,50);
                btn[i].BackColor = Color.Black;
                btn[i].ForeColor = Color.White;
                if (i == btn.Length - 1)
                {
                    btn[i].BackColor = Color.DarkBlue;
                    btn[i].ForeColor = Color.NavajoWhite;
                }
                btn[i].Text = titleForButton[i];
                this.splitContainer2.Panel1.Controls.Add(btn[i]);
                btn[i].Click += new EventHandler(btnClick);
            }
            created = true;
        }

        private void splitContainer2_Panel1_Resize(object sender, EventArgs e)
        {
            if (created) 
                for(int i=0;i<btn.Length;i++)
                {
                    btn[i].Width = splitContainer2.Panel1.Width - 40;
                }
        }

        void btnClick(object sender, EventArgs e)
        {  
            splitContainer2.Panel2.Controls.Clear();
            Button bt=(Button)sender;

            if (bt.Text == btn[0].Text)
            {
                Frm作業_1 newF =new Frm作業_1();
                newF.TopLevel = false;
                newF.Visible = true;
                splitContainer2.Panel2.Controls.Add(newF);
            }
            else if(bt.Text == btn[1].Text)
            {
                Frm作業_2 newF = new Frm作業_2();
                newF.TopLevel = false;
                newF.Visible = true;
                splitContainer2.Panel2.Controls.Add(newF);
            }
            else if (bt.Text == btn[2].Text)
            {
                Frm作業_3 newF = new Frm作業_3();
                newF.TopLevel = false;
                newF.Visible = true;
                splitContainer2.Panel2.Controls.Add(newF);
            }
            else if (bt.Text == btn[3].Text)
            {
                Frm考試 newF = new Frm考試();
                newF.TopLevel = false;
                newF.Visible = true;
                splitContainer2.Panel2.Controls.Add(newF);
            }

        }
    }
}
