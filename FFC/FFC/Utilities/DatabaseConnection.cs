using System;
using System.Collections.Generic;
using System.Text;
using FFC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FFC.Utilities
{
    public class ReferenceContext : DbContext
    {

        //private string _connectionString = "Server=tcp:fruitflyserver.database.windows.net,1433;Initial Catalog=FruitFly;Persist Security Info=False;User ID=dalleman;Password=Frugtflue1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public DbSet<Reference> References { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlite("Data Source = reference.db");
            //ob.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Reference>().HasKey(c => new { c.xPoint, c.yPoint });
        }

    }
}
