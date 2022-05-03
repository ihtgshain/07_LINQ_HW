using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqLabs;

//entity data model 特色
//1. App.config 連接字串
//2. Package 套件下載, 參考 EntityFramework.dll, EntityFramework.SqlServer.dll
//3. 導覽屬性 關聯

//4. DataSet model 需要處理 DBNull; Entity Model  不需要處理 DBNull (DBNull 會被 ignore)
//5. IQuerable<T> query 執行時會轉成 => T-SQL

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NorthwindEntities dbContext = new NorthwindEntities();
            var q = dbContext.Products.Where(n => n.UnitPrice > 30).ToList();
            dataGridView1.DataSource = q;
        }
    }
}
