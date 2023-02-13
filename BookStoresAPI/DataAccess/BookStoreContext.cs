using BookStoresAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoresAPI.DataAccess
{
    public class BookStoreContext : DbContext 
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Press> Presses { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(t => t.Id);
            modelBuilder.Entity<Press>().HasKey(t => t.Id);
        }

    }
}
