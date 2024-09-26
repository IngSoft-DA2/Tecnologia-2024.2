using LibraryProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
    }
}