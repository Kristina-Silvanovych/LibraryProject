using DataLayer.Entityes;
using Library.Models;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthor _authorService;
        private readonly IBook _bookService;

        public AuthorController(
            IAuthor authorService,
            IBook bookService
            )
        {
            _authorService = authorService;
            _bookService = bookService;
        }

        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthorBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var bookAlreadyExists = _authorService.ExistAuthor(model.Surname, model.Name);
                if (bookAlreadyExists)
                {
                    ModelState.AddModelError(nameof(model.Surname), "Author is already exist");
                    return View(model);
                }
                var newAuthor = new Author()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    FathersName = model.FathersName,
                    BirthDate = model.BirthDate.Date
                };
                await _authorService.CreateAuthor(newAuthor);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> AuthorsList()
        {
            var author = await this._authorService.GetAuthor();

            if (author != null && author.Any())
            {
                return View(author);
            }
            else
            {
                return View("EmptyList");
            }

            Response.Cookies.Delete("SURNAME");
        }

        public IActionResult ChangingAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangingAuthor(WhichAuthorWantChangeBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var authorOrNull = await _authorService.FindAuthorBySurname(model.Surname);
                if (authorOrNull is Author author)
                {
                        Response.Cookies.Append("SURNAME", model.Surname);
                        return RedirectToAction("AuthorPage", "Author");
                }
                ModelState.AddModelError("", "Wrong surname or name!!!");
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult ChangeAuthorsData()
        {
            return View();
        }

        public async Task<IActionResult> AuthorPage()
        {
            if (Request.Cookies.ContainsKey("SURNAME"))
            {
                string surname = Request.Cookies["SURNAME"];
                var currentAuthor = await _authorService.FindAuthorBySurname(surname);

                return View(currentAuthor);
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAuthorsData(ChangeAuthorsDataBindingModel model)
        {
            if (Request.Cookies.ContainsKey("SURNAME"))
            {
                string surname = Request.Cookies["SURNAME"];
                var currentAuthor = await _authorService.FindAuthorBySurname(surname);
                if (currentAuthor != null)
                {
                    if (model.Name != null) { currentAuthor.Name = model.Name; }
                    if (model.Surname != null) { currentAuthor.Surname = model.Surname; }
                    if (model.FathersName != null) { currentAuthor.FathersName = model.FathersName; }
                    if (model.BirthDate.ToString().Length > 0) { currentAuthor.BirthDate = model.BirthDate; }
                }

                var updateAuthor = await _authorService.UpdateAuthor(currentAuthor);

                return View("AuthorPage", updateAuthor);
            }


            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAuthor()
        {
            if (Request.Cookies.ContainsKey("SURNAME"))
            {
                string surname = Request.Cookies["SURNAME"];
                var currentAuthor = await _authorService.FindAuthorBySurname(surname);

                await _authorService.DeleteAuthor(currentAuthor.Id);

                return RedirectToAction("AuthorsList", "Author");
            }

            return BadRequest();
        }
    }
}

