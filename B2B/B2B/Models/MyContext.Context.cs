﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace B2B.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyEntities : DbContext
    {
        public MyEntities()
            : base("name=MyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<shopping_cart> shopping_cart { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<UserRegistration> UserRegistrations { get; set; }
        public DbSet<po> poes { get; set; }
        public DbSet<supplier_inv> supplier_inv { get; set; }
        public DbSet<product> products { get; set; }
    }
}
