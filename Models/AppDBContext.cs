
using Microsoft.EntityFrameworkCore;
using Money_Finder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedular.Helpers;

namespace TaskSchedular.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        { }
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }
    

        public DbSet<provider> provider { get; set; }
        public DbSet<source> source { get; set; }
        public DbSet<sourcelead> sourcelead { get; set; }
        public DbSet<Campaigns> Campaigns { get; set; }
        public DbSet<sendoncosmolead> sendoncosmolead { get; set; }
        public DbSet<forccgcosmolead> forccgcosmolead { get; set; }
        public DbSet<sourcelist> sourcelist { get; set; }
        public DbSet<AdminLogin> AdminLogin { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            var connectionString = Config.GetConnectionString("SQLConnection");
             options.UseSqlServer(connectionString);
        }

    }
}
