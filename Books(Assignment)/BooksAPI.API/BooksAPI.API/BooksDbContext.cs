using BooksAPI.API.Entities;
using BooksAPI.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.API
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext()
        {

        }
        public BooksDbContext(DbContextOptions options) : base(options) { }
        public DbSet<BooksModel> Book { get; set; }
        public DbSet<AddCart> Cart { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseSqlServer(@"Server=RADHIKA;Database=BooksDb;Trusted_Connection=True;");
        }
    }
}
