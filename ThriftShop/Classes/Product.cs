using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ThriftShop.Classes
{
    class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public string bname { get; set; }

        public void addNewProduct(string pname, string pcat, string pprice, string bname)
        {
            SqlConnection conn = new SqlConnection(ThriftShop.Properties.Settings.Default.thriftdbConnectionString);
            thriftLinqDataContext db = new thriftLinqDataContext(conn);
            product pro = new product();
            pro.name = pname;
            pro.price = Convert.ToDouble(pprice);
            pro.category = pcat;
            var q = from b in db.brands where b.name == bname select b.Id;
            pro.IDbrand = q.First();
            db.products.InsertOnSubmit(pro);
            db.SubmitChanges();
        }
        public IEnumerable<object> showAllProducts()
        {
            SqlConnection conn = new SqlConnection(ThriftShop.Properties.Settings.Default.thriftdbConnectionString);
            thriftLinqDataContext db = new thriftLinqDataContext(conn);
            var showAll = (from p in db.products join b in db.brands on p.IDbrand equals b.Id where p.IDbrand == b.Id select new { Name = p.name, Category = p.category, Price = p.price, Brand = b.name }).ToList<object>();
            return showAll.ToList();
            
        }
        public IEnumerable<object> FilterByPrice(string price)
        {
            SqlConnection conn = new SqlConnection(ThriftShop.Properties.Settings.Default.thriftdbConnectionString);
            thriftLinqDataContext db = new thriftLinqDataContext(conn);
            double pric = Convert.ToDouble(price);
            var filter = (from p in db.products join b in db.brands on p.IDbrand equals b.Id where (p.IDbrand == b.Id && p.price <= pric ) select new { Name = p.name, Category = p.category, Price = p.price, Brand = b.name }).ToList<object>();
            return filter.ToList();
        }
    }
}
