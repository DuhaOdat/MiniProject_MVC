﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My_e_commerceProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class storeEntities1 : DbContext
    {
        public storeEntities1()
            : base("name=storeEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<cart_item> cart_item { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<order_item> order_item { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<transaction> transactions { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
