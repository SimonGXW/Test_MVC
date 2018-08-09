using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Test_MVC.Models;

namespace Test_MVC.Data_Access_Layer
{
    public class SalesERPDAL : DbContext
    {

        public SalesERPDAL() : base("NewName"){}
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }
    }
}