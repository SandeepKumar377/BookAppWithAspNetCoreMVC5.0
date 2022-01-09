using DiverseBookApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public interface IBookRepository
    {
        Task<int> AddBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<List<BookModel>> GetTopBooks(int count);
        List<BookModel> SearchBook(string bookName, string authorName);
        Task<string> UploadFile(string folderPath, IFormFile file);
    }
}