using DiverseBookApp.Data;
using DiverseBookApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public class BookRepository
    {
        private readonly BookAppContext _context = null;

        public BookRepository(BookAppContext context)
        {
            _context = context;
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
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        //Get all book data
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        Description = book.Description,
                        LanguageId = book.LanguageId,
                        Language = book.Language.Name,
                        Id = book.Id,
                    });
                }
            }
            return books;
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
                }).FirstOrDefaultAsync();
        }
        

    //Search book method
    public List<BookModel> SearchBook(string bookName, string authorName)
    {
        return null;
    }
}
}
 