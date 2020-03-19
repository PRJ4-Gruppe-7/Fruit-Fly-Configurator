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

        private string _connectionString = "Source";
        public DbSet<Reference> References { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Reference>().HasKey(c => new { c.xPoint, c.yPoint });
        }

    }
}
