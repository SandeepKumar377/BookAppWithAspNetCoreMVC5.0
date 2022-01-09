using DiverseBookApp.Data;
using DiverseBookApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public class BookRepository
    {
        private readonly BookAppContext _context = null;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookRepository(BookAppContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        //Add book method 
        public async Task<int> AddBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl,
            };

            newBook.BookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.BookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    Url = file.Url,
                });
            }
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        //Get all book data
        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books.Select(book => new BookModel()
            {
                Id = book.Id,
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Title = book.Title,
                Language = book.Language.Name,
                LanguageId = book.LanguageId,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl,
            }).ToListAsync();
        }
        //Get Top Books data
        public async Task<List<BookModel>> GetTopBooks()
        {
            return await _context.Books.Select(book => new BookModel()
            {
                Id = book.Id,
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Title = book.Title,
                Language = book.Language.Name,
                LanguageId = book.LanguageId,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl,
            }).Take(6).ToListAsync();
        }

        //Get book data by Id
        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    Description = book.Description,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Id = book.Id,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.BookGallery.Select(g => new GalleryImagesModel()
                    {
                        Id=g.Id,
                        Name=g.Name,
                        Url=g.Url,
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl,
                }).FirstOrDefaultAsync();
        }

        //Search book method
        public List<BookModel> SearchBook(string bookName, string authorName)
        {
            return null;
        }
        public async Task<string> UploadFile(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }
    }
}
