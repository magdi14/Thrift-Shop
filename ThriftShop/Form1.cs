using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ThriftShop.Classes;
using System.Data.Linq;

namespace ThriftShop
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(ThriftShop.Properties.Settings.Default.thriftdbConnectionString);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thriftLinqDataContext db = new thriftLinqDataContext(conn);
            var q = from a in db.brands select new { Name = a.name, Products = a.numOfproducts };
            dataGridView1.DataSource = q;
            fillCompoBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Brand b = new Brand();
            b.addNewBrand(addnewBrand.Text);
            addnewBrand.Clear();
            MessageBox.Show("Brand Added Successfully!");
            thriftLinqDataContext db = new thriftLinqDataContext(conn);
            var q = from a in db.brands select new { Name = a.name };
            dataGridView1.DataSource = q;
            fillCompoBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            string sel = this.ProductBrand.GetItemText(this.ProductBrand.SelectedItem);
            prod.addNewProduct(productName.Text, ProductCate.Text, ProductPrice.Text, sel);
            productName.Clear();
            ProductCate.Clear();
            ProductPrice.Clear();
            MessageBox.Show("Product Added Successfully!");
        }
        private void fillCompoBox()
        {
            thriftLinqDataContext db = new thriftLinqDataContext(conn);
            var q = db.brands.Select(c => new { c.name });
            ProductBrand.DataSource = q.ToList();
            ProductBrand.ValueMember = "Name";
            sortBy.Items.Add("Price");
            sortBy.Items.Add("Name");
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            dataGridView1.DataSource =  p.showAllProducts();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            string pr = InputPrice.Text;
            dataGridView1.DataSource = p.FilterByPrice(pr);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Brand b = new Brand();
            dataGridView1.DataSource = b.showSortedBrands();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            string sel = this.sortBy.GetItemText(this.sortBy.SelectedItem);
            string wa = "";
            bool isChecked1 = ascen.Checked;
            //bool isChecked2 = Descending.Checked;
            if (isChecked1)
                wa = ascen.Text;
            else
                wa = descen.Text;
            dataGridView1.DataSource = p.sortProduct(sel, wa);
            wa = "";
        }
    }
}
