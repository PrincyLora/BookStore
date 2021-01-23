using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practise.BookStoreApp.Data
{
    public class BookStoreContext: DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {

        }
        public DbSet<Books> Books { get; set; } //entity class
        public  DbSet<Language> Language { get; set; }

        public DbSet<BookGallery> BookGallery { get; set; }


        //for configuring db conn either you can use below class or set in staartup.cs
        /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
              optionsBuilder.UseSqlServer("Server=.;Database:BookStore;Integrated Security=True");

              base.OnConfiguring(optionsBuilder);
          }*/


    }
}
