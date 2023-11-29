using DataLayer.Entityes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EFDBContext : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }

        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options) { }

        //For Migrations
        public class EFDBContextFactory : IDesignTimeDbContextFactory<EFDBContext>
        {
            public EFDBContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<EFDBContext>();
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = BooksDB;Integrated Security=True;TrustServerCertificate=True", b => b.MigrationsAssembly("DataLayer"));
                return new EFDBContext(optionsBuilder.Options);
            }
        }

    }
}
