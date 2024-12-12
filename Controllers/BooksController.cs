using BooksLibrary.Data;
using BooksLibrary.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BooksLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;

        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var books = context.Books.OrderByDescending(p => p.Id).ToList();
            return View(books);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }

            Book book1 = new Book()
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Description = book.Description,
                AddedAt = DateTime.Now,
            };

            context.Books.Add(book1);
            context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        [Authorize]
        public IActionResult Edit(int Id)
        {
            var book = context.Books.Find(Id);

            if (book == null) 
            {
                return RedirectToAction("Index", "Books");
            }

            var book2 = new Book()
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Description = book.Description,
                AddedAt = DateTime.Now,
            };

            ViewData["Bookid"] =book.Id;
            ViewData["AddedAt"] =book.AddedAt.ToString("dd/MM/yyyy");

            return View(book2);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int Id, Book book)
        {
            // Find the existing book by ID
            var bookToUpdate = context.Books.Find(Id);

            if (bookToUpdate == null)
            {
                // If the book does not exist, redirect to Index
                return RedirectToAction("Index", "Books");
            }

            if (!ModelState.IsValid)
            {
                // If model validation fails, return the current view with the book data
                return View(book);
            }

            // Update the fields of the existing book with the new values
            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Genre = book.Genre;
            bookToUpdate.Description = book.Description;

            // Save the changes to the database
            context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        [Authorize]
        public IActionResult Delete(int Id)
        {
            var book = context.Books.Find(Id);
            if (book == null)
            {
                return RedirectToAction("Index", "Books");
            }

            context.Books.Remove(book);
            context.SaveChanges(true);

            return RedirectToAction("Index", "Books");
        }
    }
}
