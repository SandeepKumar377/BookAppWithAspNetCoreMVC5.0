using DiverseBookApp.Data;
using DiverseBookApp.Models;
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
        public int AddBook(BookModel model)
        {
            var newBook = new Books()
            {   
                Author = model.Author,
                Title = model.Title,
                Description = model.Description,
                CreatedOn   = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                TotalPages = model.TotalPages,
            };
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return newBook.Id;
        }

        //Get book data
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        //Get book data by Id
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        //Search book method
        public List<BookModel> SearchBook(string bookName, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(bookName) || x.Author.Contains(authorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){ Id=1,Language="English", TotalPages=1033, Category="Pro" ,Title="MVC", Author="Sandeep", Description="This is MVC Description"},
                new BookModel(){ Id=2,Language="French", TotalPages=103, Category="Prog" ,Title="JAVA", Author="Kumar", Description="This is JAVA Description"},
                new BookModel(){ Id=3,Language="Hindi", TotalPages=1043, Category="Data" ,Title="html", Author="Sendy", Description="This is HTML Description"},
                new BookModel(){ Id=4,Language="English", TotalPages=133, Category="ML" ,Title="php", Author="Sandeep4", Description="This is PHP Description"},
                new BookModel(){ Id=5,Language="Chines", TotalPages=1833, Category="MI" ,Title="python", Author="Sandeep5", Description="This is Python Description"},
            };
        }
    }
}
 