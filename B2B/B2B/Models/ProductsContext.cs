using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace B2B.Models
{
    [Table("products")]
    public class ProductsContext : DbContext
    {
        public DbSet<Products> Products {get ; set ;}
    }
}