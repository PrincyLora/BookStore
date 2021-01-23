using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using practise.BookStoreApp.Models;
using practise.BookStoreApp.Repository;

namespace practise.BookStoreApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ViewResult> GetAllBooks()
        {
            var data= await _bookRepository.GetAllBooks();
            return View(data);
        }

        public async Task<BookModel> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return data;
        }

        public List<BookModel> SearchBooks(string bookName , string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
            
        }

        [Route("book-details/{id}", Name = "bookDetailsRoute")]
        public async Task<ViewResult> GetBookDetail(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId =0)
        {
            var model = new BookModel()
            {
                //Language = "2"
            };
            //var group1 = new SelectListGroup() { Name = "Group 1" };
            //var group2 = new SelectListGroup() { Name = "Group 2" };
            //var group3 = new SelectListGroup() { Name = "Group 3" };

            // ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");
            //ViewBag.Language = GetLanguage().Select(x => new SelectListItem()
            //{
            //    Text = x.Text,
            //    Value= x.Id.ToString()

            //}).ToList();

            //ViewBag.Language = new List<SelectListItem>()
            //{
            // new SelectListItem(){Text="Hindi", Value="1", Group = group1},
            // new SelectListItem(){Text="English", Value="2",Group = group1},
            // new SelectListItem(){Text="Dutch", Value="3", Group = group2},
            // new SelectListItem(){Text="Kannada", Value="4",Group = group2},
            // new SelectListItem(){Text="Konkani", Value="5", Group = group3},
            // new SelectListItem(){Text="Tulu", Value="6", Group = group3},

            //};
            ViewBag.Language =new SelectList( await _languageRepository.GetLanguages(),"Id","Text");
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
           
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageURL= await UploadImage(folder, bookModel.CoverPhoto);
                }
                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();

                    foreach(var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
            //var group1 = new SelectListGroup() { Name = "Group 1" };
            //var group2 = new SelectListGroup() { Name = "Group 2" };
            //var group3 = new SelectListGroup() { Name = "Group 3" };
            //ViewBag.IsSuccess = false;
            //ViewBag.BookId = 0;
            // ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");
            //ViewBag.Language = new List<SelectListItem>()
            //{
            // new SelectListItem(){Text="Hindi", Value="1", Group = group1},
            // new SelectListItem(){Text="English", Value="2",Group = group1},
            // new SelectListItem(){Text="Dutch", Value="3", Group = group2},
            // new SelectListItem(){Text="Kannada", Value="4",Group = group2},
            // new SelectListItem(){Text="Konkani", Value="5", Group = group3},
            // new SelectListItem(){Text="Tulu", Value="6", Group = group3},
            //};
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Text");
            ModelState.AddModelError("", "This is custom error message");
            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverfolder, FileMode.Create));

            return "/" + folderPath;
        }

        //hardcoded select values
        //private List<LanguageModel> GetLanguage()
        //{
        //    return new List<LanguageModel>()
        //    {
        //         new LanguageModel(){Id=1, Text="Hindi"},
        //         new LanguageModel(){Id=2, Text="Kannada"},
        //         new LanguageModel(){Id=3, Text="English"}
        //    };
        //}
    }
}
