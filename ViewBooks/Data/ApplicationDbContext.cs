using Microsoft.EntityFrameworkCore;
using ViewBooks.Controllers;
using ViewBooks.Models;
using Category = ViewBooks.Models.Category;

namespace ViewBooks.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
		{

        }
        public DbSet<Book> Books { get; set; }
		public DbSet<Category> Categorys { get; set; }

	}
}
