﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ThriftShop.Classes
{
    class Brand
    {
        public int id { get; set; }
        public string BrandName { get; set; }

        public void addNewBrand(string name)
        {
            SqlConnection conn = new SqlConnection(ThriftShop.Properties.Settings.Default.thriftdbConnectionString);
            thriftLinqDataContext db = new thriftLinqDataContext(conn);
            brand b = new brand();
            b.name = name;
            db.brands.InsertOnSubmit(b);
            db.SubmitChanges();
        }
        
        
    }
}