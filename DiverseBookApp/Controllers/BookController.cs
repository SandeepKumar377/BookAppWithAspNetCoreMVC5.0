﻿using DiverseBookApp.Models;
using DiverseBookApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiverseBookApp.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ViewResult GetAllBooks()
        {
            var data= _bookRepository.GetAllBooks();
            return View(data);
        }

        public ViewResult GetBook(int id)
        {

            var data = _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        public ViewResult AddNewBook(bool isSuccess=false, int bookId=0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }
        
        [HttpPost]
        public IActionResult AddNewBook(BookModel bookModel)
        {
           var id= _bookRepository.AddBook(bookModel);
            if (id>0)
            {
                return RedirectToAction(nameof(AddNewBook), new {isSuccess=true, bookId =id});
            }
            return View();
        }
    }
}
