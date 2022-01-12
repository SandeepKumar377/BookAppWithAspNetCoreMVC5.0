using DiverseBookApp.Models;
using DiverseBookApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiverseBookApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;


        public BookController(IBookRepository bookRepository,
            ILanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
        }

        //Get all book GET method
        [Route("all-books")]
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        //Get book by Id GET method
        [Route("book-details/{id:int:min(1)}")]
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }


        //Add new Book Get method
        [HttpGet]
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguage(), "Id", "Name");
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        //Add new book POST method 
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "BookAppData/CoverPhoto/";
                    bookModel.CoverImageUrl=await _bookRepository.UploadFile(folder, bookModel.CoverPhoto);
                }
                if (bookModel.GalleryImages != null)
                {
                    string folder = "BookAppData/GalleryImages/";
                    bookModel.Gallery = new List<GalleryImagesModel>();
                    foreach (var file in bookModel.GalleryImages)
                    {
                        var gallery = new GalleryImagesModel()
                        {
                            Name = file.FileName,
                            Url = await _bookRepository.UploadFile(folder, file),
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }
                if (bookModel.BookPdf != null)
                {
                    string folder = "BookAppData/BookPDF/";
                    bookModel.BookPdfUrl = await _bookRepository.UploadFile(folder, bookModel.BookPdf);
                }
                var id = await _bookRepository.AddBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguage(), "Id", "Name");
            return View();
        }       
    }
}
