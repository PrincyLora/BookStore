using Microsoft.EntityFrameworkCore;
using practise.BookStoreApp.Data;
using practise.BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace practise.BookStoreApp.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;


        //dependency injection example below
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                Pages = model.Pages.HasValue ? model.Pages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverImageURL = model.CoverImageURL

            };
            //  var gallery = new List<BookGallery>();
            newBook.bookGallery = new List<BookGallery>();
            foreach(var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name=file.Name,
                    URL = file.URL
                });
            }
            await  _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }
        //below method retrives data from list.
        /*public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }*/

        //below method throws error when data is null in db table
        public async  Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.Books.ToListAsync();
            if (allBooks?.Any() == true)
            {
                foreach(var book in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description= book.Description,
                        Id=book.Id,
                        LanguageId=book.LanguageId,
                       // Language =book.Language.Text,
                        Title=book.Title,
                        Pages=book.Pages,
                        CoverImageURL=book.CoverImageURL
                    }) ;
                }
            }
            return books;
        }
        //below book gets detail by ID from list
        /* public BookModel GetBookById(int id)
         {
             return DataSource().Where(x => x.Id == id).FirstOrDefault();
         }*/
        public async Task<BookModel> GetBookById(int id)
        {
            // var book = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();

            // var book = await _context.Books.FindAsync(id);
            return await _context.Books.Where(x => x.Id == id).Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Language = book.Language.Text,
                Title = book.Title,
                Pages = book.Pages,
                CoverImageURL = book.CoverImageURL,
                Gallery = book.bookGallery.Select(g => new GalleryModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    URL = g.URL
                }).ToList()
            }).FirstOrDefaultAsync();

            //if (book != null)
            //{
            //    var bookDetails = new BookModel()
            //    {
            //        Author = book.Author,
            //        Category = book.Category,
            //        Description = book.Description,
            //        Id = book.Id,
            //        LanguageId = book.LanguageId,
            //        Language = book.Language.Text,
            //        Title = book.Title,
            //        Pages = book.Pages

            //    };
            //    return bookDetails;
            //}
          //  return null;
        }
        public List<BookModel> SearchBook(string title, string authorName)
        {
            
            return null;

           
        }

        //hardcoded values
        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){Id=1,Title= "as", Author="Abdul Kalam", Description="A memoir every Indian should read!", Category="Biography", Language="English", Pages=365},
        //        new BookModel(){Id=2,Title= "Zahir", Author="Paulo Coelho" ,Description="Bullshit. Don't read this. Waste of time. Author is crazy.", Category="Fiction", Language="Brazil", Pages=299},
        //        new BookModel(){Id=3,Title= "ATME", Author="Khaleed", Description="A sweet heartwarming and heartbreaking story of a boy set in Afganistan", Category="Fiction", Language="English", Pages=554},
        //        new BookModel(){Id=4,Title= "If Tomorrow Comes", Author="Sidney Sheldon", Description="A thriller novel which keeps you on edge till the last page and leaves you mesmerized.", Category="Thriller", Language="English", Pages=456},

        //    };
        //}
    }
}
