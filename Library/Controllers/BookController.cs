using DataLayer.Entityes;
using Library.Models;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IAuthor _authorService;
        private readonly IBook _bookService;

        public BookController(
            IAuthor authorService,
            IBook bookService
            )
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var bookAlreadyExists = _bookService.ExistBook(model.Title);
                if (bookAlreadyExists)
                {
                    ModelState.AddModelError(nameof(model.Title), "Book is already exist");
                    return View(model);
                }
                var newBook = new Book()
                {
                    Title = model.Title,
                    PageCount = model.CountOfPages,
                    Genre = model.Genre,
                    AuthorId = model.AuthorId
                };
                await _bookService.CreateBook(newBook);
                return RedirectToAction("BooksList");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> BooksList()
        {
            var book = await this._bookService.GetBook();

            if (book != null && book.Any())
            {
                return View(book);
            }
            else
            {
                return View("EmptyList");
            }

            Response.Cookies.Delete("TITLE");
        }

        public IActionResult ChangingBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangingBook(WhatBookWantChangeBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var bookOrNull = await _bookService.FindBookByTitle(model.Title);
                if (bookOrNull is Book book)
                {
                    Response.Cookies.Append("TITLE", model.Title);
                    return RedirectToAction("BookPage", "Book");
                }
                ModelState.AddModelError("", "Wrong title!!!");
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult ChangeBooksData()
        {
            return View();
        }

        public async Task<IActionResult> BookPage()
        {
            if (Request.Cookies.ContainsKey("TITLE"))
            {
                string title = Request.Cookies["TITLE"];
                var currentBook = await _bookService.FindBookByTitle(title);

                return View(currentBook);
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeBooksData(ChangeBooksDataBindingModel model)
        {

            if (Request.Cookies.ContainsKey("TITLE"))
            {
                string title = Request.Cookies["TITLE"];
                var currentBook = await _bookService.FindBookByTitle(title);
                if (currentBook != null)
                {
                    if (model.Title != null) { currentBook.Title = model.Title; }
                    if (model.PagesCount != 0) { currentBook.PageCount = model.PagesCount; }
                    if (model.Genre != null) { currentBook.Genre = model.Genre; }
                    if (model.AuthorId != 0) { currentBook.AuthorId = model.AuthorId; }
                }

                var updateBook = await _bookService.UpdateBook(currentBook);

                return View("BookPage", updateBook);
            }
            return View("Error");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveBook()
        {

            if (Request.Cookies.ContainsKey("TITLE"))
            {
                string title = Request.Cookies["TITLE"];
                var currentBook = await _bookService.FindBookByTitle(title);

                await _bookService.DeleteBook(currentBook.Id);

                return RedirectToAction("BooksList", "Book");
            }

            return BadRequest();
        }

        public async Task<IActionResult> BooksOfCurrentAuthor()
        {
            if (Request.Cookies.ContainsKey("SURNAME"))
            {
                string surname = Request.Cookies["SURNAME"];
                var currentAuthor = await _authorService.FindAuthorBySurname(surname);
                var books = await this._bookService.FindBookByAuthorId(currentAuthor.Id);
                if (books != null && books.Any())
                {
                    return View(books);
                }
                else
                { 
                    return View("EmptyList");
                }
            }
            return View("Error");
        }
    }
}

