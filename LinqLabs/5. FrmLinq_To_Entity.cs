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
            dbContext.Database.Log = Console.Write; //開著佔效能，沒用時可註解掉
        }
        //todo 0504 1 confirm the Entity Data Model
        NorthwindEntities dbContext = new NorthwindEntities();

        private void button1_Click(object sender, EventArgs e)
        {
            var q = dbContext.Products.Where(n => n.UnitPrice > 30).ToList();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = q;
        }

        //todo 0504 2 navigation properties
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dbContext.Categories.First().Products.ToList();
            MessageBox.Show(dbContext.Products.First().Category.CategoryName);
        }

        //todo 0504 3 stored procedure (add it into NWModel.edmx before using)
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dbContext.Sales_by_Year(new DateTime(1997, 1, 1), DateTime.Now).ToList();

        }
        //todo 0504 4 OrderBy ThenBy (descending)  ※Entity can't support my method or so C# method. use AsEnumerable() before using.
        private void button22_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dbContext.Products.OrderByDescending(p => p.UnitsInStock).ThenBy(p => p.ProductID).ToList();

            dataGridView2.DataSource = null;
            //dataGridView2.DataSource=dbContext.Products.OrderBy(p => p.UnitsInStock).ThenByDescending(p => p.ProductID).ToList();


            //System.NotSupportedException: 'LINQ to Entities 無法辨識方法 'System.String Format(System.String, System.Object)' 方法，而且這個方法無法轉譯成存放區運算式。'
            dataGridView2.DataSource=dbContext.Products.AsEnumerable().OrderBy(p => p.UnitsInStock).ThenByDescending(p => p.ProductID)
                .Select(p=>new {p.ProductID,p.ProductName,p.UnitPrice,p.UnitsInStock,Total=$"{p.UnitsInStock * p.UnitPrice:C2}"}).ToList();

        }
        //todo 0504 5 navigation property
        private void button16_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            //get CategoryName though navigation property "Category"
            dataGridView3.DataSource = dbContext.Products.Select(p => new { p.CategoryID, p.Category.CategoryName, p.ProductName, p.ProductID }).ToList();
        }

        //todo 0504 5 inner join
        private void button20_Click(object sender, EventArgs e)
        {
            var q = from c in dbContext.Categories
                    from p in c.Products  //inner join can omit categoryID without productsand products with out CategoryID
                    select new { c.CategoryID, c.CategoryName, p.ProductID, p.ProductName };
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = q.ToList();


        }
        //todo 0504 6 GroupBy and Navigation
        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dbContext.Products.GroupBy(g => g.Category.CategoryName)
                .Select(g => new { CategoryName = g.Key, AVG = g.Average(n => n.UnitPrice) })
                .Where(n => n.AVG != 0).ToList();
        }
        //todo 0504 7 again
        private void button14_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dbContext.Orders.GroupBy(y => y.OrderDate.Value.Year)
                .Select(y => new { Year = y.Key, Count = y.Count() }).ToList();
        }
        //todo 0504 8 insert
        private void button55_Click(object sender, EventArgs e)
        {
                        //table.Add()
            dbContext.Products.Add(new Product { ProductName = DateTime.Now.ToLongDateString(), Discontinued = true });
            dbContext.SaveChanges();
            //DB.SaveChanges()
        }
    }
}
