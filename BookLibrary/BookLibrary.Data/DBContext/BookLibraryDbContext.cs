using BookLibrary.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Data.DBContext
{
    public class BookLibraryDbContext : IdentityDbContext<User>
    {
        public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options):base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genre { get; set; }
    }
}
